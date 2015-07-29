﻿using GameStore.Domain.Abstract;
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
        private IOrderProcessor _orderProcessor;
        public CartController(IGameRepository repository, IOrderProcessor orderProcessor)
        {
            this._repository = repository;
            this._orderProcessor = orderProcessor;
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

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if(cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Your cart is empty");
            }
            if(ModelState.IsValid)
            {
                _orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}