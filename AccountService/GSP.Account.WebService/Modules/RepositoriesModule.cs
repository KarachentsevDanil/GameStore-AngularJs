using Autofac;
using GSP.Accounts.DAL.Context;
using GSP.Accounts.DAL.Repositories;
using GSP.Accounts.DAL.Repositories.Contracts;
using GSP.Accounts.DAL.UnitOfWork;
using GSP.Accounts.DAL.UnitOfWork.Contracts;

namespace GSP.Accounts.WebService.Modules
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameStoreAccountContext>().As<GameStoreAccountContext>().InstancePerLifetimeScope();

            builder.RegisterType<GameStoreAccountUnitOfWork>().As<IGameStoreAccountUnitOfWork>().PropertiesAutowired();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
        }
    }
}
