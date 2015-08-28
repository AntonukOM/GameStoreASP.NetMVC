using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using GameStore.WebUI.Models;
using System;
using System.Web.Mvc;
using GameStore.WebUI.HtmlHelpers;
using GameStore.WebUI.Controllers;

namespace GameStore.UnitTests
{
    [TestClass]
    public class PaginateTest
    {
        [TestMethod]
        public void CanPaginate()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Game 1"},
                new Game { GameId = 2, Name = "Game 2"},
                new Game { GameId = 3, Name = "Game 3"},
                new Game { GameId = 4, Name = "Game 4"},
                new Game { GameId = 5, Name = "Game 5"}
            });
            GameController controller = new GameController(mock.Object);
            controller.PageSize = 3;

            //act
            GamesListViewModel res = (GamesListViewModel)controller.GameList(null, 2).Model;

            //assert
            List<Game> list = res.Games.ToList();
            Assert.IsTrue(list.Count == 2);
            Assert.AreEqual(list[0].Name, "Game 4");
            Assert.AreEqual(list[1].Name, "Game 5");
        }

        [TestMethod]
        public void CanGeneratePageLinks()
        {
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;
           
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void CanSendPaginationViewModel()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Game 1"},
                new Game { GameId = 2, Name = "Game 2"},
                new Game { GameId = 3, Name = "Game 3"},
                new Game { GameId = 4, Name = "Game 4"},
                new Game { GameId = 5, Name = "Game 5"}
            });
            GameController controller = new GameController(mock.Object);
            controller.PageSize = 3;

            // Act
            GamesListViewModel result
                = (GamesListViewModel)controller.GameList(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void CanFilterGames()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Game 1", Category = "Cat 1"},
                new Game { GameId = 2, Name = "Game 2", Category = "Cat 2"},
                new Game { GameId = 3, Name = "Game 3", Category = "Cat 1"},
                new Game { GameId = 4, Name = "Game 4", Category = "Cat 2"},
                new Game { GameId = 5, Name = "Game 5", Category = "Cat 3"}
            });
            GameController controller = new GameController(mock.Object);

            controller.PageSize = 3;
            List<Game> result = ((GamesListViewModel)controller.GameList("Cat 2", 1).Model)
                .Games.ToList();

            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Game 2" && result[0].Category == "Cat 2");
            Assert.IsTrue(result[1].Name == "Game 4" && result[1].Category == "Cat 2");
        }

        [TestMethod]
        public void CanCreateCategories()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game> {
                new Game { GameId = 1, Name = "Game 1", Category = "RPG"},
                new Game { GameId = 2, Name = "Game 2", Category = "Simulator"},
                new Game { GameId = 3, Name = "Game 3", Category = "Shooter"},
                new Game { GameId = 4, Name = "Game 4", Category = "RPG"},
            });

            NavController target = new NavController(mock.Object);
  
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();
            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "RPG");
            Assert.AreEqual(results[1], "Shooter");
            Assert.AreEqual(results[2], "Simulator");
        }

        [TestMethod]
        public void IndicatesSelectedCategory()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
                    mock.Setup(m => m.Games).Returns(new Game[] {
                new Game { GameId = 1, Name = "Game 1", Category = "Simulator"},
                new Game { GameId = 2, Name = "Game 2", Category = "Shooter"}
            });

            NavController target = new NavController(mock.Object);

            string categoryToSelect = "Shooter";

            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void GenerateCategorySpecificGameCount()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Game 1", Category = "Ctg 1"},
                new Game { GameId = 2, Name = "Game 2", Category = "Ctg 2"},
                new Game { GameId = 3, Name = "Game 3", Category = "Ctg 1"},
                new Game { GameId = 4, Name = "Game 4", Category = "Ctg 2"},
                new Game { GameId = 5, Name = "Game 5", Category = "Ctg 3"}
            });
            GameController controller = new GameController(mock.Object);
            controller.PageSize = 3;

            int res1 = ((GamesListViewModel)controller.GameList("Ctg 1").Model).PagingInfo.TotalItems;
            int res2 = ((GamesListViewModel)controller.GameList("Ctg 2").Model).PagingInfo.TotalItems;
            int res3 = ((GamesListViewModel)controller.GameList("Ctg 3").Model).PagingInfo.TotalItems;
            int resAll = ((GamesListViewModel)controller.GameList(null).Model).PagingInfo.TotalItems;

            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}