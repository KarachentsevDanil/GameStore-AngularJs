using GSP.Account.DAL.EF.Context;
using GSP.Account.Domain.Customers;
using GSP.WebApi.Configurations;
using GSP.WebApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GSP.Account.WebApi.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddIdentityAuthorization(this IServiceCollection services, AuthenticationConfiguration configuration)
        {
            services.AddIdentity<Customer, IdentityRole>(o =>
                {
                    o.Password.RequireDigit = false;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<CustomerContext>()
                .AddDefaultTokenProviders();
            
            services.AddJwtAuthentication(configuration);

            return services;
        }
    }
}