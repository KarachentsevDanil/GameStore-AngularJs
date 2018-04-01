using Autofac;
using GSP.Games.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GSP.Games.WebService.Modules
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(CreateDbContextOptions).As<DbContextOptions<GameStoreGameContext>>();
        }

        private static DbContextOptions<GameStoreGameContext> CreateDbContextOptions(IComponentContext context)
        {
            var configuration = context.Resolve<IConfiguration>();
            string connectionString = configuration["Data:DefaultConnection:ConnectionString"];

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<GameStoreGameContext>();
            dbContextOptionsBuilder.UseSqlServer(connectionString);

            using (var dbContext = new GameStoreGameContext(dbContextOptionsBuilder.Options))
            {
                dbContext.Database.EnsureCreated();
            }

            return dbContextOptionsBuilder.Options;
        }
        
    }
}
