using System.Collections.Generic;
using GSP.Domain.Orders;
using GSP.Domain.Params;

namespace GSP.DAL.Repositories.Contracts
{
    public interface IOrderRepository : IGameStoreRepository<Order>
    {
        IEnumerable<Order> GetOrders();

        IEnumerable<Order> GetOrdersByParams(FilterParams<Order> filterParams, out int totalCount);

        IEnumerable<Order> GetCustomerOrders(int customerId);

        Order GetCurrentCustomerOrder(int customerId);

        void AddGameToBucket(OrderGame game);

        void DeleteGameFromBucket(int id);
    }
}
