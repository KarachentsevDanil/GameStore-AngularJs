using Autofac;
using GSP.AprioriAlgoritm.Contracts;
using GSP.AprioriAlgoritm.Service;
using GSP.Common.BLL.Services.Cache;
using GSP.Common.BLL.Services.Contracts;
using GSP.Orders.BLL.Services;
using GSP.Orders.BLL.Services.Contracts;

namespace GSP.Orders.WebService.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<RecomendationService>().As<IRecomendationService>();
            builder.RegisterType<OrderCacheService>().As<IOrderCacheService>();
            builder.RegisterType<CacheService>().As<ICacheService>();
            builder.RegisterType<PaymentService>().As<IPaymentService>();
        }
    }
}
