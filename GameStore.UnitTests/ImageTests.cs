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
    public class ImageTests
    {
        [TestMethod]
        public void CanRetreiveImageData()
        {
            Game game = new Game
            {
                GameId = 2,
                Name = "Game 2",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game> {
                new Game {GameId = 1, Name = "Game 1"},
                game,
                new Game {GameId = 3, Name = "Game 3"}
            }.AsQueryable());

            GameController controller = new GameController(mock.Object);
            
            ActionResult result = controller.GetImage(2);
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(game.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void CanNotRetreiveImageData()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game> {
                new Game {GameId = 1, Name = "Game 1"},
                new Game {GameId = 2, Name = "Game 2"}
            }.AsQueryable());

            GameController controller = new GameController(mock.Object);
            
            ActionResult result = controller.GetImage(10);
            
            Assert.IsNull(result);
        }
    }
}
