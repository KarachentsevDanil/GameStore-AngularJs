using GSP.Common.BLL.Services.Cache;
using GSP.Orders.BLL.Services.Contracts;

namespace GSP.Orders.BLL.Services
{
    public class OrderCacheService : CacheService, IOrderCacheService
    {
        public void ResetBucket(string key)
        {
            Remove(key, CacheBucket.Bucket);
        }

        public void ResetPayments(string key)
        {
            Remove(key, CacheBucket.Payments);
        }

        public void ResetCustomerGames(string key)
        {
            Remove(key, CacheBucket.CustomerGames);
        }
    }
}
