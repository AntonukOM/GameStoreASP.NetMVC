using GameStore.Domain.Entities;
using System.Data.Entity;

namespace GameStore.Domain.Concrete
{
    public class GameDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
    }
}
