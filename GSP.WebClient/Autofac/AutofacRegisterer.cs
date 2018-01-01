using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GSP.WebClient.Autofac
{
    internal class AutofacRegisterer : IAutofacRegisterer
    {
        private readonly ContainerBuilder builder;

        public AutofacRegisterer()
        {
            builder = new ContainerBuilder();
        }

        public void RegisterModules(IEnumerable<AssemblyName> assemblyNames)
        {
            var assemblies = assemblyNames
                .Where(name => name.Name.StartsWith("GSP"))
                .Distinct()
                .Select(Assembly.Load);

            builder.RegisterAssemblyModules(assemblies.ToArray());
        }

        public void RegisterConfiguration(IConfiguration configuration)
        {
            builder.Register(context => configuration);
        }

        public void Populate(IServiceCollection services)
        {
            builder.Populate(services);
        }

        public IServiceProvider GetServiceProvider()
        {
            return builder.Build().Resolve<IServiceProvider>();
        }
    }
}
