using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System.Collections.Generic;
namespace GameStore.Domain.Concrete
{
    public class GameDbRepository : IGameRepository
    {
        private GameDbContext _context = new GameDbContext();
        public IEnumerable<Game> Games
        {
            get { return _context.Games; }
        }

        public void SaveGame(Game game)
        {
            if(game.GameId == 0)
            {
                _context.Games.Add(game);
            }
            else
            {
                Game entryGame = _context.Games.Find(game.GameId);
                if(entryGame != null)
                {
                    entryGame.Name = game.Name;
                    entryGame.Description = game.Description;
                    entryGame.Category = game.Category;
                    entryGame.Price = game.Price;
                }                
            }
            _context.SaveChanges();
        }




        public Game Delete(int gameId)
        {
            Game entryGame = _context.Games.Find(gameId);
            if(entryGame != null)
            {
                _context.Games.Remove(entryGame);
                _context.SaveChanges();
            }
            return entryGame;
        }
    }
}
