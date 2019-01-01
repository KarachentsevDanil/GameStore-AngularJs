using GSP.Domain.Games;
using GSP.Rate.DAL.EF.Context.Mappings;
using Microsoft.EntityFrameworkCore;

namespace GSP.DAL.EF.Context
{
    public class RateContext : DbContext
    {
        public RateContext(DbContextOptions<RateContext> options) : base(options)
        { }
        
        public DbSet<RateBase> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            new RateMapping().MapEntity(builder.Entity<RateBase>());
        }
    }
}