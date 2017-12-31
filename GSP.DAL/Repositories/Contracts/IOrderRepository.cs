using System.Collections.Generic;
using GSP.Domain.Orders;

namespace GSP.DAL.Repositories.Contracts
{
    public interface IOrderRepository : IGameStoreRepository<Order>, IExperssionable<Order>
    {
        IEnumerable<Order> GetOrders();

        IEnumerable<Order> GetCustomerOrders(int customerId);

        Order GetCurrentCustomerOrder(int customerId);

        void AddGameToBucket(OrderGame game);

        void DeleteGameFromBucket(int id);
    }
}
