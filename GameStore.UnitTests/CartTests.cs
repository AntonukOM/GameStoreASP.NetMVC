using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GameStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void CanAddNewLines()
        {
            Game game1 = new Game { GameId = 1, Name = "Game 1" };
            Game game2 = new Game { GameId = 2, Name = "Game 2" };

            Cart cart = new Cart();
            cart.AddItem(game1, 2);
            cart.AddItem(game2, 1);

            List<CartLine> res = cart.Lines.ToList();

            Assert.AreEqual(res.Count, 2);
            Assert.AreEqual(res[0].Game, game1);
            Assert.AreEqual(res[1].Game, game2);
        }

        [TestMethod]
        public void CanAddQuantityForExitingLines()
        {
            Game game1 = new Game { GameId = 1, Name = "Game 1" };
            Game game2 = new Game { GameId = 2, Name = "Game 2" };

            Cart cart = new Cart();

            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);
            List<CartLine> results = cart.Lines.OrderBy(c => c.Game.GameId).ToList();

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void CanRemoveLine()
        {
            Game game1 = new Game { GameId = 1, Name = "Game 1" };
            Game game2 = new Game { GameId = 2, Name = "Game 2" };
            Game game3 = new Game { GameId = 3, Name = "Game 3" };

            Cart cart = new Cart();
            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);
            cart.AddItem(game3, 2);

            cart.RemoveLine(game2);
            Assert.AreEqual(cart.Lines.Count(), 2);
            Assert.AreEqual(cart.Lines.Where(g => g.Game == game2).Count(), 0);
        }

        [TestMethod]
        public void CalculateTotalCart()
        {
            Game game1 = new Game { GameId = 1, Name = "Game 1", Price = 100 };
            Game game2 = new Game { GameId = 2, Name = "Game 2", Price = 200 };

            Cart cart = new Cart();
            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);

            decimal totalCost = cart.ComputeTotalValue();
            Assert.AreEqual(totalCost, 800);
        }
     
        [TestMethod]
        public void ClearCart()
        {
            Game game1 = new Game { GameId = 1, Name = "Game 1", Price = 100 };
            Game game2 = new Game { GameId = 2, Name = "Game 2", Price = 200 };
            Cart cart = new Cart();
            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);
            cart.Clear();
            Assert.AreEqual(cart.Lines.Count(), 0);            
        }

        [TestMethod]
        public void CanAddToCart()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game> {
                new Game {GameId = 1, Name = "Game 1", Category = "Ctg 1"},
            }.AsQueryable());

            Cart cart = new Cart();

            CartController controller = new CartController(mock.Object, null);

            controller.AddToCart(cart, 1, null);

            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToList()[0].Game.GameId, 1);
        }

        [TestMethod]
        public void AddingGameToCartGoesToCartScreen()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
                mock.Setup(m => m.Games).Returns(new List<Game> {
                    new Game {GameId = 1, Name = "Game 1", Category = "Ctg 1"},
                }.AsQueryable());

            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object, null);
            RedirectToRouteResult result = controller.AddToCart(cart, 2, "myUrl");

            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["Url"], "myUrl");
        }

        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            Cart cart = new Cart();

            CartController target = new CartController(null, null);

            CartIndexViewModel result
                = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.Url, "myUrl");
        }

        [TestMethod]
        public void CannotCheckoutEmptyCart()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();

            ShippingDetails shippingDetails = new ShippingDetails();
            CartController controller = new CartController(null, mock.Object);
            ViewResult result = controller.Checkout(cart, shippingDetails);

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Never());

            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void CannotCheckoutInvalidShippingDetails()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Game(), 1);

            CartController controller = new CartController(null, mock.Object);        
            controller.ModelState.AddModelError("error", "error");

            ViewResult result = controller.Checkout(cart, new ShippingDetails());
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Never());
         
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void CanCheckoutAndSubmitOrder()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Game(), 1);

            CartController controller = new CartController(null, mock.Object);
            ViewResult result = controller.Checkout(cart, new ShippingDetails());

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Once());

            Assert.AreEqual("Completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }
    }
}
