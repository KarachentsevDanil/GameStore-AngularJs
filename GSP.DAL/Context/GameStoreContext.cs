using GSP.DAL.Context.Mappings;
using GSP.Domain.Customers;
using GSP.Domain.Games;
using GSP.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace GSP.DAL.Context
{
    public class GameStoreContext : DbContext
    {
        public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
        {
            
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DbSet<OrderGame> OrderGames { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            new CategoryMapping().MapEntity(builder.Entity<Category>());
            new CustomerMapping().MapEntity(builder.Entity<Customer>());
            new GameMapping().MapEntity(builder.Entity<Game>());
            new OrderGameMapping().MapEntity(builder.Entity<OrderGame>());
            new OrderMapping().MapEntity(builder.Entity<Order>());
            new RateMapping().MapEntity(builder.Entity<Rate>());

            base.OnModelCreating(builder);
        }
    }
}
