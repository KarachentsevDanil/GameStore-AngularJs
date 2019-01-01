using GSP.Account.DAL.EF.Context.Mappings;
using GSP.Account.Domain.Customers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GSP.Account.DAL.EF.Context
{
    public class CustomerContext : IdentityDbContext<Customer>
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        { }
        
        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            new CustomerMapping().MapEntity(builder.Entity<Customer>());
        }
    }
}