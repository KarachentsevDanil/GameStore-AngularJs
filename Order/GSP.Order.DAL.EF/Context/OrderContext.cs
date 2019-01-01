using GSP.Order.DAL.EF.Context.Mappings;
using GSP.Order.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace GSP.Order.DAL.EF.Context
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        { }
        
        public DbSet<OrderBase> Orders { get; set; }

        public DbSet<OrderGame> OrderGames { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            new OrderGameMapping().MapEntity(builder.Entity<OrderGame>());
            new OrderMapping().MapEntity(builder.Entity<OrderBase>());
        }
    }
}