using GSP.Common.BLL.Services.Cache;
using GSP.Games.BLL.Services.Contracts;

namespace GSP.Games.BLL.Services
{
    public class GameCacheService : CacheService, IGameCacheService
    {
        public void ResetCategories()
        {
            EmptyBucket(CacheBucket.Categories);
        }
    }
}
