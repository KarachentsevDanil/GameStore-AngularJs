using GSP.Payment.BLL.Services;
using GSP.Payment.BLL.Services.Contracts;
using GSP.Payment.DAL.EF.Repositories;
using GSP.Payment.DAL.EF.UnitOfWork;
using GSP.Payment.DAL.Repositories.Contracts;
using GSP.Payment.DAL.UnitOfWork.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace GSP.Payment.WebApi.Extensions
{
    public static class DependencyInjectionsExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPaymentUnitOfWork, PaymentUnitOfWork>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IPaymentService, PaymentService>();

            return services;
        }
    }
}