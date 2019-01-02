using GSP.Account.BLL.Services;
using GSP.Account.BLL.Services.Contracts;
using GSP.Account.DAL.EF.Repositories;
using GSP.Account.DAL.EF.UnitOfWork;
using GSP.Account.DAL.Repositories.Contracts;
using GSP.Account.DAL.UnitOfWork.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace GSP.Account.WebApi.Extensions
{
    public static class DependencyInjectionsExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerUnitOfWork, CustomerUnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }
    }
}