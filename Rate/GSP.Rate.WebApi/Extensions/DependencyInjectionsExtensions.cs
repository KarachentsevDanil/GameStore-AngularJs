using GSP.DAL.EF.UnitOfWork;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Rate.BLL.Services.Contracts;
using GSP.Rate.DAL.EF.Repositories;
using GSP.Rate.DAL.Repositories.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace GSP.Rate.WebApi.Extensions
{
    public static class DependencyInjectionsExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRateUnitOfWork, RateUnitOfWork>();
            services.AddScoped<IRateRepository, RateRepository>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRateService, IRateService>();

            return services;
        }
    }
}