using GSP.Domain.Params;
using GSP.Order.BLL.DTOs.Order;
using GSP.Order.Domain.Params;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Order.BLL.Services.Contracts
{
    public interface IOrderService
    {
        Task<int> AddOrderAsync(CreateOrderDto order, CancellationToken ct = default);

        Task UpdateOrderAsync(CompleteOrderDto order, CancellationToken ct = default);

        Task DeleteOrderAsync(int orderId, CancellationToken ct = default);

        Task AddGameToBucketAsync(AddGameToBucketDto game, CancellationToken ct = default);

        Task DeleteGameFromBucketAsync(AddGameToBucketDto game, CancellationToken ct = default);

        Task<IEnumerable<OrderGameDto>> GetGameFromBucketAsync(string customerId, CancellationToken ct = default);
        
        Task<CollectionResult<OrderDto>> GetOrdersByParamsAsync(OrdersFilterParams filterParams, CancellationToken ct = default);

        Task<OrderDto> GetCurrentOrderOfCustomerAsync(string customerId, CancellationToken ct = default);

        Task<IEnumerable<OrderGameDto>> GetCustomerGamesAsync(string customerId, CancellationToken ct = default);
    }
}