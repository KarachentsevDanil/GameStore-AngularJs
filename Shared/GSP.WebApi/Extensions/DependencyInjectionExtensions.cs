using GSP.WebApi.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GSP.WebApi.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static DatabaseConfiguration AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConfig = new DatabaseConfiguration();
            configuration.Bind("DatabaseConfiguration", dbConfig);
            services.AddSingleton(dbConfig);

            return dbConfig;
        }

        public static IServiceCollection ConfigureDatabase<TContext>(this IServiceCollection services, DatabaseConfiguration csConfig)
            where TContext : DbContext
        {
            services.AddDbContext<TContext>(options => options.UseSqlServer(csConfig.ConnectionString));
            return services;
        }

        public static IApplicationBuilder InitializeMigration<TContext>(this IApplicationBuilder app)
            where TContext : DbContext
        {
            using (IServiceScope serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (TContext context = serviceScope.ServiceProvider.GetService<TContext>())
                {
                    context.Database.Migrate();
                    context.Database.EnsureCreated();
                }
            }

            return app;
        }
    }
}