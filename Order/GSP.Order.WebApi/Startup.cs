using AutoMapper;
using GSP.Order.BLL.Configurations.MapperProfiles;
using GSP.Order.DAL.EF.Context;
using GSP.Order.WebApi.Extensions;
using GSP.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GSP.Order.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var dbConfig = services.AddDatabaseConfiguration(Configuration);

            services.ConfigureDatabase<OrderContext>(dbConfig);

            services.RegisterRepositories();

            services.RegisterServices();

            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<OrderAutoMapperProfile>();
            });

            services.AddSingleton(c => config.CreateMapper());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.InitializeMigration<OrderContext>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}