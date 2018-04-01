using GSP.Common.Web.Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace GSP.Common.Web.Configuration
{
    public static class CommonServiceConfiguration
    {
        public static void AddCommonConfiguration(this IServiceCollection services)
        {
            services.AddCors();

            services.AddAutofac();
        }
    }
}
