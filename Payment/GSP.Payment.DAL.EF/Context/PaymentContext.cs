using GSP.Payment.DAL.EF.Context.Mappings;
using GSP.Payment.Domain.Payments;
using Microsoft.EntityFrameworkCore;

namespace GSP.Payment.DAL.EF.Context
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        { }

        public DbSet<PaymentBase> Payments{ get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            new PaymentMapping().MapEntity(builder.Entity<PaymentBase>());
        }
    }
}