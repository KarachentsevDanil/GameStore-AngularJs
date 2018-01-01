using Autofac;
using GSP.BLL.Services;
using GSP.BLL.Services.Contracts;
using GSP.DAL.Repositories.Contracts;

namespace GSP.WebClient.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<RateService>().As<IRateService>();
        }
    }
}
