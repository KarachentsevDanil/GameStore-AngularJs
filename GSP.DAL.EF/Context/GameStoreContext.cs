using GSP.DAL.EF.Context.Mappings;
using GSP.Domain.Customers;
using GSP.Domain.Games;
using GSP.Domain.Orders;
using GSP.Domain.Payments;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GSP.DAL.EF.Context
{
    public class GameStoreContext : IdentityDbContext<Customer>
    {
        public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
        { }

        public DbSet<Game> Games { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DbSet<Payment> Payments{ get; set; }

        public DbSet<OrderGame> OrderGames { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new CategoryMapping().MapEntity(builder.Entity<Category>());
            new CustomerMapping().MapEntity(builder.Entity<Customer>());
            new GameMapping().MapEntity(builder.Entity<Game>());
            new OrderGameMapping().MapEntity(builder.Entity<OrderGame>());
            new OrderMapping().MapEntity(builder.Entity<Order>());
            new RateMapping().MapEntity(builder.Entity<Rate>());
            new GamePhotoMapping().MapEntity(builder.Entity<GamePhoto>());
            new PaymentMapping().MapEntity(builder.Entity<Payment>());
        }
    }
}
