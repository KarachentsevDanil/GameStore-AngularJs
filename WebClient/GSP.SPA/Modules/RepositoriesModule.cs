using Autofac;
using GSP.DAL.Context;
using GSP.DAL.Repositories;
using GSP.DAL.Repositories.Contracts;
using GSP.DAL.UnitOfWork;
using GSP.DAL.UnitOfWork.Contracts;

namespace GSP.SPA.Modules
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameStoreContext>().As<GameStoreContext>().InstancePerLifetimeScope();

            builder.RegisterType<GameStoreUnitOfWork>().As<IGameStoreUnitOfWork>().PropertiesAutowired();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<GameRepository>().As<IGameRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<RateRepository>().As<IRateRepository>();
            builder.RegisterType<PaymentRepository>().As<IPaymentRepository>();
        }
    }
}
