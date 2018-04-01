using GSP.Accounts.DAL.Context;
using GSP.Accounts.Domain.Customers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GSP.Accounts.WebService.Authentication
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddIdentityAuthorization(this IServiceCollection services)
        {
            services.AddIdentity<Customer, IdentityRole>(o =>
                {
                    o.Password.RequireDigit = false;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequiredLength = 6;

                })
                .AddEntityFrameworkStores<GameStoreAccountContext>();
            
            return services;
        }
    }
}
