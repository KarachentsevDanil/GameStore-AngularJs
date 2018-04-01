using Autofac;
using GSP.Games.DAL.Context;
using GSP.Games.DAL.Repositories;
using GSP.Games.DAL.Repositories.Contracts;
using GSP.Games.DAL.UnitOfWork;
using GSP.Games.DAL.UnitOfWork.Contracts;

namespace GSP.Games.WebService.Modules
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameStoreGameContext>().As<GameStoreGameContext>().InstancePerLifetimeScope();

            builder.RegisterType<GameStoreGameUnitOfWork>().As<IGameStoreGameUnitOfWork>().PropertiesAutowired();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<GameRepository>().As<IGameRepository>();
            builder.RegisterType<RateRepository>().As<IRateRepository>();
        }
    }
}
