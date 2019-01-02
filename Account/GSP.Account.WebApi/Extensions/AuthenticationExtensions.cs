using GSP.Account.DAL.EF.Context;
using GSP.Account.Domain.Customers;
using GSP.SPA.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

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
                .AddEntityFrameworkStores<CustomerContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = configuration.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });

            return services;
        }

        public static AuthenticationConfiguration AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new AuthenticationConfiguration();

            configuration.Bind("AuthenticationConfiguration", config);
            services.AddSingleton(config);

            return config;
        }

        public static SecurityKey GetSymmetricSecurityKey(this AuthenticationConfiguration configuration)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.TokenKey));
        }
    }
}