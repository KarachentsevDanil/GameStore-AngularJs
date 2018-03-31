using System.Collections.Generic;
using GSP.Common.DAL.Repositories.Contracts;
using GSP.Orders.Domain.Orders;
using GSP.Orders.Domain.Params;

namespace GSP.Orders.DAL.Repositories.Contracts
{
    public interface IOrderRepository : IGameStoreRepository<Order>
    {
        IEnumerable<Order> GetOrdersByParams(OrdersFilterParams filterParams, out int totalCount);

        Order GetCurrentCustomerOrder(string customerId);

        void AddGameToBucket(OrderGame game);

        void DeleteGameFromBucket(int id);
    }
}
