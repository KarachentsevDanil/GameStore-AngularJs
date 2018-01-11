using System.Collections.Generic;
using GSP.Domain.Games;
using GSP.Domain.Orders;
using GSP.Domain.Params;

namespace GSP.BLL.Services.Contracts
{
    public interface IOrderService
    {
        void AddOrder(Order order);

        void UpdateOrder(Order order);

        void DeleteOrder(int orderId);

        void AddGameToBucket(OrderGame game);

        void DeleteGameFromBucket(OrderGame game);

        IEnumerable<Game> GetGameFromBucket(int customerId);

        IEnumerable<Game> GetCustomerGames(int customerId);

        IEnumerable<Order> GetOrdersByParams(OrdersFilterParams filterParams, out int totalCount);

        Order GetCurrentOrderOfCustomer(int customerId);
    }
}
