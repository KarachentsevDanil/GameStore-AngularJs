using GSP.Accounts.DAL.Context.Mappings;
using GSP.Accounts.Domain.Customers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GSP.Accounts.DAL.Context
{
    public class GameStoreAccountContext : IdentityDbContext<Customer>
    {
        public GameStoreAccountContext(DbContextOptions<GameStoreAccountContext> options) : base(options)
        { }
        
        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            new CustomerMapping().MapEntity(builder.Entity<Customer>());
        }
    }
}
