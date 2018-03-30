using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyModel;

namespace GSP.SPA.Autofac
{
    public static class AutofacExtensions
    {
        public static IServiceCollection AddAutofac(this IServiceCollection services)
        {
            var autofacRegisterer = new AutofacRegisterer();
            autofacRegisterer.Populate(services);

            var serviceDescriptor = new ServiceDescriptor(typeof(IAutofacRegisterer), autofacRegisterer);
            services.TryAdd(serviceDescriptor);

            return services;
        }

        public static IApplicationBuilder UseAutofac(this IApplicationBuilder applicationBuilder, IConfiguration configuration)
        {
            var runtime = DependencyContext.Default.Target.Runtime;
            var assemblyNames = DependencyContext.Default.GetRuntimeAssemblyNames(runtime);

            var autofacRegisterer = applicationBuilder.ApplicationServices.GetService<IAutofacRegisterer>();
            autofacRegisterer.RegisterConfiguration(configuration);
            autofacRegisterer.RegisterModules(assemblyNames);

            applicationBuilder.ApplicationServices = autofacRegisterer.GetServiceProvider();
            return applicationBuilder;
        }
    }
}
