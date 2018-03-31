using GSP.Orders.DAL.Context.Mappings;
using GSP.Orders.Domain.Orders;
using GSP.Orders.Domain.Payments;
using Microsoft.EntityFrameworkCore;

namespace GSP.Orders.DAL.Context
{
    public class GameStoreOrderContext : DbContext
    {
        public GameStoreOrderContext(DbContextOptions<GameStoreOrderContext> options) : base(options)
        { }
        
        public DbSet<Order> Orders { get; set; }

        public DbSet<Payment> Payments{ get; set; }

        public DbSet<OrderGame> OrderGames { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            new OrderGameMapping().MapEntity(builder.Entity<OrderGame>());
            new OrderMapping().MapEntity(builder.Entity<Order>());
            new PaymentMapping().MapEntity(builder.Entity<Payment>());
        }
    }
}
