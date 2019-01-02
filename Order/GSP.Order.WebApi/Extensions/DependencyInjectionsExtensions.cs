using GSP.DAL.EF.UnitOfWork;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Order.BLL.Services;
using GSP.Order.BLL.Services.Contracts;
using GSP.Order.DAL.EF.Repositories;
using GSP.Order.DAL.Repositories.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace GSP.Order.WebApi.Extensions
{
    public static class DependencyInjectionsExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderUnitOfWork, OrderUnitOfWork>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}