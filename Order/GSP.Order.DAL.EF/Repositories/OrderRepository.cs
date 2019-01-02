using GSP.DAL.EF.Repositories;
using GSP.Domain.Params;
using GSP.Order.DAL.EF.Context;
using GSP.Order.DAL.Repositories.Contracts;
using GSP.Order.Domain.Enums;
using GSP.Order.Domain.Orders;
using GSP.Order.Domain.Params;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Order.DAL.EF.Repositories
{
    public class OrderRepository : BaseRepository<int, OrderBase, OrderContext>, IOrderRepository
    {
        private readonly OrderContext _dbContext;

        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CollectionResult<OrderBase>> GetOrdersByParamsAsync(OrdersFilterParams filterParams, CancellationToken ct = default)
        {
            var collectionResult = new CollectionResult<OrderBase>();

            var query = GetAllOrders();

            FillOrdersQueryFilterParams(filterParams);
            query = query.Where(filterParams.Expression).OrderByDescending(t => t.OrderId);

            collectionResult.TotalCount = query.Count();

            collectionResult.Collection = await query
                .Skip(filterParams.PageSize * (filterParams.PageNumber - 1))
                .Take(filterParams.PageSize)
                .OrderByDescending(x => x.OrderId)
                .ToListAsync(ct);

            return collectionResult;
        }

        public async Task<OrderBase> GetCurrentCustomerOrderAsync(string customerId, CancellationToken ct = default)
        {
            return await GetAllOrders().FirstOrDefaultAsync(x => x.Status == OrderStatus.New && x.CustomerId == customerId, ct);
        }

        public async Task AddGameToBucketAsync(OrderGame game, CancellationToken ct = default)
        {
            await _dbContext.OrderGames.AddAsync(game, ct);
        }

        public async Task DeleteGameFromBucketAsync(int id, CancellationToken ct = default)
        {
            var game = await _dbContext.OrderGames.FindAsync(id, ct);
            _dbContext.OrderGames.Remove(game);
        }

        public async Task<IEnumerable<OrderGame>> GetCustomerGamesAsync(string customerId, CancellationToken ct = default)
        {
            var games = await _dbContext.OrderGames.Where(t => t.Order.CustomerId == customerId).ToListAsync(ct);
            return games;
        }

        private void FillOrdersQueryFilterParams(OrdersFilterParams filterParams)
        {
            var predicate = PredicateBuilder.New<OrderBase>(x => x.Status == OrderStatus.Complete);

            if (!string.IsNullOrEmpty(filterParams.CustomerId))
            {
                predicate = predicate.Extend(x => x.CustomerId == filterParams.CustomerId, PredicateOperator.And);
            }

            filterParams.Expression = predicate;
        }

        private IQueryable<OrderBase> GetAllOrders()
        {
            return _dbContext.Orders
                .Include(x => x.Games)
                .Where(x => !x.IsDeleted).AsQueryable();
        }
    }
}
