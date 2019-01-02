using GSP.DAL.Repositories.Contracts;
using GSP.Domain.Params;
using GSP.Order.Domain.Orders;
using GSP.Order.Domain.Params;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Order.DAL.Repositories.Contracts
{
    public interface IOrderRepository : IGenericRepository<int, OrderBase>
    {
        Task<CollectionResult<OrderBase>> GetOrdersByParamsAsync(OrdersFilterParams filterParams, CancellationToken ct = default);

        Task<OrderBase> GetCurrentCustomerOrderAsync(string customerId, CancellationToken ct = default);

        Task AddGameToBucketAsync(OrderGame game, CancellationToken ct = default);

        Task DeleteGameFromBucketAsync(int id, CancellationToken ct = default);

        Task<IEnumerable<OrderGame>> GetCustomerGamesAsync(string customerId, CancellationToken ct = default);
    }
}