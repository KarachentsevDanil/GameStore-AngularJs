using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GSP.Account.BLL.Configurations.MapperProfiles;
using GSP.Account.DAL.EF.Context;
using GSP.Account.WebApi.Extensions;
using GSP.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GSP.Account.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var authConfig = services.AddAuthenticationConfiguration(Configuration);
            
            services.AddIdentityAuthorization(authConfig);

            var dbConfig = services.AddDatabaseConfiguration(Configuration);

            services.ConfigureDatabase<CustomerContext>(dbConfig);

            services.RegisterRepositories();

            services.RegisterServices();

            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<CustomerAutoMapperProfile>();
            });

            services.AddSingleton(c => config.CreateMapper());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.InitializeMigration<CustomerContext>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
