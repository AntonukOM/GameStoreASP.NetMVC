using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void IndexContainsAllGames()
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
            AdminController controller = new AdminController(mock.Object);

            List<Game> res = ((IEnumerable<Game>)controller.Index()
                .ViewData.Model).ToList();
            Assert.AreEqual(res.Count(), 5);
            Assert.AreEqual("Game 1", res[0].Name);
            Assert.AreEqual("Game 2", res[1].Name);
            Assert.AreEqual("Game 3", res[2].Name);
        }

        [TestMethod]
        public void CanEdit()
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

            AdminController controller = new AdminController(mock.Object);

            Game game1 = controller.Edit(1).ViewData.Model as Game;
            Game game2 = controller.Edit(2).ViewData.Model as Game;
            Game game3 = controller.Edit(3).ViewData.Model as Game;
            
            Assert.AreEqual(1, game1.GameId);
            Assert.AreEqual(2, game2.GameId);
            Assert.AreEqual(3, game3.GameId);
        }

        [TestMethod]
        public void CanSaveValidChanges()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            AdminController controller = new AdminController(mock.Object);
            Game game = new Game { Name = "Test" };
            ActionResult result = controller.Edit(game);
            mock.Verify(m => m.SaveGame(game));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void CannotSaveInvalidChanges()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            AdminController controller = new AdminController(mock.Object);
            Game game = new Game { Name = "Test" };
            controller.ModelState.AddModelError("error", "error");
            ActionResult result = controller.Edit(game);
            mock.Verify(m => m.SaveGame(It.IsAny<Game>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
      
    }
}
