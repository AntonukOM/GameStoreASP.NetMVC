using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System.Collections.Generic;
namespace GameStore.Domain.Concrete
{
    public class GameDbRepository : IGameRepository
    {
        private GameDbContext context = new GameDbContext();
        public IEnumerable<Game> Games
        {
            get { return context.Games; }
        }
    }
}
