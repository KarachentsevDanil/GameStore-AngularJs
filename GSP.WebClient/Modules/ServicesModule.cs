using Autofac;
using GSP.AprioriAlgoritm.Contracts;
using GSP.AprioriAlgoritm.Service;
using GSP.BLL.Services;
using GSP.BLL.Services.Contracts;

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
            builder.RegisterType<RecomendationService>().As<IRecomendationService>();
        }
    }
}
