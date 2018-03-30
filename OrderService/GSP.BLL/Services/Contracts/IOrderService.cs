using System.Collections.Generic;
using GSP.BLL.Dto.Game;
using GSP.BLL.Dto.Order;
using GSP.Domain.Params;

namespace GSP.BLL.Services.Contracts
{
    public interface IOrderService
    {
        int AddOrder(CreateOrderDto order);

        void UpdateOrder(CompleteOrderDto order);

        void DeleteOrder(int orderId);

        void AddGameToBucket(AddGameToBucketDto game);

        void DeleteGameFromBucket(AddGameToBucketDto game);

        IEnumerable<GameDto> GetGameFromBucket(string customerId);

        IEnumerable<GameDto> GetCustomerGames(string customerId);

        IEnumerable<OrderDto> GetOrdersByParams(OrdersFilterParams filterParams, out int totalCount);

        OrderDto GetCurrentOrderOfCustomer(string customerId);
    }
}
