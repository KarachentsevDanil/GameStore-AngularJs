using System.Collections.Generic;
using GSP.Domain.Orders;
using GSP.Domain.Params;

namespace GSP.DAL.Repositories.Contracts
{
    public interface IOrderRepository : IGameStoreRepository<Order>
    {
        IEnumerable<Order> GetOrdersByParams(OrdersFilterParams filterParams, out int totalCount);

        Order GetCurrentCustomerOrder(int customerId);

        void AddGameToBucket(OrderGame game);

        void DeleteGameFromBucket(int id);
    }
}
