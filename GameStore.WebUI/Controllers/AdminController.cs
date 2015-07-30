using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System.Web.Mvc;
using System.Linq;

namespace GameStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IGameRepository _repository;
        public AdminController(IGameRepository repository)
        {
            this._repository = repository;
        }
        // GET: Admin
        public ViewResult Index()
        {
            return View(_repository.Games);
        }
        public ViewResult Edit(int gameId) //the parametr name is same as class field name
        {
            Game game = _repository.Games
                .FirstOrDefault(g => g.GameId == gameId);
            return View(game);
        }

        [HttpPost]
        public ActionResult Edit(Game game)
        {
            if(ModelState.IsValid)
            {
                _repository.SaveGame(game);
                TempData["message"] = string.Format("Changes is the game \"{0}\" was saved", game.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(game);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Game());
        }

        [HttpPost]
        public ActionResult Delete(int gameId)
        {
            Game deletedGame = _repository.Delete(gameId);
            if (deletedGame != null)
            {
                TempData["message"] = string.Format("Game \"{0}\" was deleted",
                    deletedGame.Name);
            }
            return RedirectToAction("Index");
        }
    }
}