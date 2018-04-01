using Autofac;
using GSP.Accounts.BLL.Services;
using GSP.Accounts.BLL.Services.Contracts;
using GSP.Common.BLL.Services.Cache;
using GSP.Common.BLL.Services.Contracts;

namespace GSP.Accounts.WebService.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<CacheService>().As<ICacheService>();
        }
    }
}
