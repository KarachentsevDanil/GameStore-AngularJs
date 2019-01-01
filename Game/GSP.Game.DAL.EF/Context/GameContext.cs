using GSP.Game.DAL.EF.Context.Mappings;
using GSP.Game.Domain.Games;
using Microsoft.EntityFrameworkCore;
using GameMapping = GSP.Game.DAL.EF.Context.Mappings.GameMapping;
using GamePhotoMapping = GSP.Game.DAL.EF.Context.Mappings.GamePhotoMapping;

namespace GSP.DAL.EF.Context
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        { }

        public DbSet<GameBase> Games { get; set; }
        
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new CategoryMapping().MapEntity(builder.Entity<Category>());
            new GameMapping().MapEntity(builder.Entity<GameBase>());
            new GamePhotoMapping().MapEntity(builder.Entity<GamePhoto>());
        }
    }
}
