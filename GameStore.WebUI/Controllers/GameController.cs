using GameStore.Domain.Abstract;
using System.Web.Mvc;
using System.Linq;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class GameController : Controller
    {
        private IGameRepository _repository;
        public int PageSize { get; set; }
        public GameController(IGameRepository repository)
        {
            this.PageSize = 4;
            this._repository = repository;
        }
        // GET: Game
        public ViewResult GameList(string category, int page = 1)
        {
            GamesListViewModel model = new GamesListViewModel
            {
                Games = _repository.Games
                    .Where(c => category == null || c.Category == category)
                    .OrderBy(game => game.GameId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = (category == null) ?
                    _repository.Games.Count() :
                    _repository.Games.Where(g => g.Category == category).Count()                    
                },
                Category = category
            };
            return View(model);
        }

    }
}