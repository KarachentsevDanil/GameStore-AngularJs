using AutoMapper;
using GSP.Payment.BLL.Configurations.MapperProfiles;
using GSP.Payment.DAL.EF.Context;
using GSP.Payment.WebApi.Extensions;
using GSP.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GSP.Payment.WebApi
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

            services.ConfigureDatabase<PaymentContext>(dbConfig);

            services.RegisterRepositories();

            services.RegisterServices();

            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<PaymentAutoMapperProfile>();
            });

            services.AddSingleton(c => config.CreateMapper());
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.InitializeMigration<PaymentContext>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
