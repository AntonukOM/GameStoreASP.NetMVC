using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameStore.Domain.Abstract;
using System.Collections;
using GameStore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Models;
using System;
using System.Web.Mvc;
using GameStore.WebUI.HtmlHelpers;

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
            GamesListViewModel res = (GamesListViewModel)controller.GameList(2).Model;

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
                = (GamesListViewModel)controller.GameList(2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
    }
}

