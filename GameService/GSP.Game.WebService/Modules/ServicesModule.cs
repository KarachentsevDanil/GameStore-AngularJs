using Autofac;
using GSP.Common.BLL.Services.Cache;
using GSP.Common.BLL.Services.Contracts;
using GSP.Games.BLL.Services;
using GSP.Games.BLL.Services.Contracts;

namespace GSP.Games.WebService.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameCacheService>().As<IGameCacheService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<RateService>().As<IRateService>();
            builder.RegisterType<CacheService>().As<ICacheService>();
        }
    }
}
