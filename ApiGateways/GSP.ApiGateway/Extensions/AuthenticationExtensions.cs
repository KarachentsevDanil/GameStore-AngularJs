using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GSP.ApiGateway.Configurations;

namespace GSP.ApiGateway.Extensions
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

        public static SecurityKey GetSymmetricSecurityKey(this AuthenticationConfiguration configuration)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.TokenKey));
        }
    }
}