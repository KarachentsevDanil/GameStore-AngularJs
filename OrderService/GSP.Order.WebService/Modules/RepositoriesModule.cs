using Autofac;
using GSP.Orders.DAL.Context;
using GSP.Orders.DAL.Repositories;
using GSP.Orders.DAL.Repositories.Contracts;
using GSP.Orders.DAL.UnitOfWork;
using GSP.Orders.DAL.UnitOfWork.Contracts;

namespace GSP.Orders.WebService.Modules
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameStoreOrderContext>().As<GameStoreOrderContext>().InstancePerLifetimeScope();

            builder.RegisterType<GameStoreOrderUnitOfWork>().As<IGameStoreOrderUnitOfWork>().PropertiesAutowired();

            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<PaymentRepository>().As<IPaymentRepository>();
        }
    }
}
