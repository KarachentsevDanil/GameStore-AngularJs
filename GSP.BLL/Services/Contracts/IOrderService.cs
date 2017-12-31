using System.Collections.Generic;
using GSP.Domain.Games;
using GSP.Domain.Orders;

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

        IEnumerable<Order> GetCustomerOrders(int customerId);

        IEnumerable<Order> GetOrders();
    }
}
