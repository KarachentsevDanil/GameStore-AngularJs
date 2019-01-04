using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GSP.WebApi.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GSP.WebApi.Extensions
{
    public static class AuthenticationExtensions
    {
        public static AuthenticationConfiguration AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new AuthenticationConfiguration();

            configuration.Bind("AuthenticationConfiguration", config);
            services.AddSingleton(config);

            return config;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, AuthenticationConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
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

        public static SecurityKey GetSymmetricSecurityKey(this AuthenticationConfiguration configuration)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.TokenKey));
        }
    }
}