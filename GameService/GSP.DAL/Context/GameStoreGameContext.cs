using GSP.Games.DAL.Context.Mappings;
using GSP.Games.Domain.Games;
using Microsoft.EntityFrameworkCore;

namespace GSP.Games.DAL.Context
{
    public class GameStoreGameContext : DbContext
    {
        public GameStoreGameContext(DbContextOptions<GameStoreGameContext> options) : base(options)
        { }

        public DbSet<Games.Domain.Games.Game> Games { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Rate> Rates { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new CategoryMapping().MapEntity(builder.Entity<Category>());
            new GameMapping().MapEntity(builder.Entity<Games.Domain.Games.Game>());
            new RateMapping().MapEntity(builder.Entity<Rate>());
            new GamePhotoMapping().MapEntity(builder.Entity<GamePhoto>());
        }
    }
}
