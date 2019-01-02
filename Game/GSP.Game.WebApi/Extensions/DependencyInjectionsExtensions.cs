using GSP.DAL.EF.UnitOfWork;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Game.BLL.Services;
using GSP.Game.BLL.Services.Contracts;
using GSP.Game.DAL.EF.Repositories;
using GSP.Game.DAL.Repositories.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace GSP.Game.WebApi.Extensions
{
    public static class DependencyInjectionsExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGameUnitOfWork, GameUnitOfWork>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}