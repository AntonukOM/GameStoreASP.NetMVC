﻿using GameStore.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IGameRepository _repository;
        public NavController(IGameRepository repository)
        {
            this._repository = repository;
        }
        public PartialViewResult Menu(string category = null, bool horizontalNav = false)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = _repository.Games
                .Select(game => game.Category)
                .Distinct()
                .OrderBy(x => x);

            //string viewName = horizontalNav ? "MenuHorizontal" : "Menu";
            return PartialView("FlexMenu", categories);
        }
    }
}