// make a game list without using database

using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System.Collections.Generic;

namespace GameStore.Domain
{
    public class LocalGameDBContext : IGameRepository
    {
        public List<Game> Games { get; set; }
        public LocalGameDBContext()
        {
            Games = new List<Game>();
            Games.Add(new Game { Name = "Game 1", Description = "Game Descriptiom 1", Price = 10 });
            Games.Add(new Game { Name = "Game 2", Description = "Game Descriptiom 2", Price = 20 });
            Games.Add(new Game { Name = "Game 3", Description = "Game Descriptiom 3", Price = 30 });
            Games.Add(new Game { Name = "Game 4", Description = "Game Descriptiom 4", Price = 40 });
            Games.Add(new Game { Name = "Game 5", Description = "Game Descriptiom 5", Price = 50 });
            Games.Add(new Game { Name = "Game 6", Description = "Game Descriptiom 6", Price = 60 });
            Games.Add(new Game { Name = "Game 7", Description = "Game Descriptiom 7", Price = 70 });
            Games.Add(new Game { Name = "Game 8", Description = "Game Descriptiom 8", Price = 80 });
            Games.Add(new Game { Name = "Game 9", Description = "Game Descriptiom 9", Price = 90 });
            Games.Add(new Game { Name = "Game 10", Description = "Game Descriptiom 10", Price = 100 });
            Games.Add(new Game { Name = "Game 11", Description = "Game Descriptiom 11", Price = 110 });
            Games.Add(new Game { Name = "Game 12", Description = "Game Descriptiom 12", Price = 120 });
            Games.Add(new Game { Name = "Game 13", Description = "Game Descriptiom 13", Price = 130 });
            Games.Add(new Game { Name = "Game 14", Description = "Game Descriptiom 14", Price = 140 });
            Games.Add(new Game { Name = "Game 15", Description = "Game Descriptiom 15", Price = 150 });
        }
                                                
        IEnumerable<Game> IGameRepository.Games
        {
            get { return  Games; }
        }
    }
}