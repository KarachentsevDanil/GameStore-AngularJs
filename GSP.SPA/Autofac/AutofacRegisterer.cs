using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GSP.SPA.Autofac
{
    internal class AutofacRegisterer : IAutofacRegisterer
    {
        private readonly ContainerBuilder _builder;

        public AutofacRegisterer()
        {
            _builder = new ContainerBuilder();
        }

        public void RegisterModules(IEnumerable<AssemblyName> assemblyNames)
        {
            var assemblies = assemblyNames
                .Where(name => name.Name.StartsWith("GSP"))
                .Distinct()
                .Select(Assembly.Load);

            _builder.RegisterAssemblyModules(assemblies.ToArray());
        }

        public void RegisterConfiguration(IConfiguration configuration)
        {
            _builder.Register(context => configuration);
        }

        public void Populate(IServiceCollection services)
        {
            _builder.Populate(services);
        }

        public IServiceProvider GetServiceProvider()
        {
            return _builder.Build().Resolve<IServiceProvider>();
        }
    }
}
