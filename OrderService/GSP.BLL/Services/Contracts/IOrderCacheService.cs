using GSP.Common.BLL.Services.Contracts;

namespace GSP.Orders.BLL.Services.Contracts
{
    public interface IOrderCacheService : ICacheService
    {
        void ResetBucket(string key);

        void ResetPayments(string key);

        void ResetCustomerGames(string key);
    }
}
