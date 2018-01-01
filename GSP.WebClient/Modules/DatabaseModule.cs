using Autofac;
using GSP.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GSP.WebClient.Modules
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(CreateDbContextOptions).As<DbContextOptions<GameStoreContext>>();
        }

        private static DbContextOptions<GameStoreContext> CreateDbContextOptions(IComponentContext context)
        {
            var configuration = context.Resolve<IConfiguration>();
            string connectionString = configuration["Data:DefaultConnection:ConnectionString"];

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<GameStoreContext>();
            dbContextOptionsBuilder.UseSqlServer(connectionString);

            using (var dbContext = new GameStoreContext(dbContextOptionsBuilder.Options))
            {
                dbContext.Database.EnsureCreated();
            }

            return dbContextOptionsBuilder.Options;
        }
        
    }
}
