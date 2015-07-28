using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IGameRepository _repository;
        public CartController(IGameRepository repository)
        {
            this._repository = repository;
        }
        // GET: Cart
        public ViewResult Index(Cart cart, string url)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                Url = url
            });
        }

        /*
        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        */
        public RedirectToRouteResult AddToCart(Cart cart, int gameId, string url)
        {
            Game game = _repository.Games.FirstOrDefault(g => g.GameId == gameId);
            if(game != null)
            {
                cart.AddItem(game, 1);
            }
            return RedirectToAction("Index", new { url });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int gameId, string url)
        {
            Game game = _repository.Games
                .FirstOrDefault(g => g.GameId == gameId);

            if (game != null)
            {
                cart.RemoveLine(game);
            }
            return RedirectToAction("Index", new { url });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
    }
}