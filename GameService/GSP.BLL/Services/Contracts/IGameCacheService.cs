using GSP.Common.BLL.Services.Contracts;

namespace GSP.Games.BLL.Services.Contracts
{
    public interface IGameCacheService : ICacheService
    {
        void ResetCategories();
    }
}
