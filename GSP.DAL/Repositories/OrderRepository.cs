using System.Collections.Generic;
using System.Linq;
using GSP.DAL.Context;
using GSP.DAL.Repositories.Contracts;
using GSP.Domain.Games;
using GSP.Domain.Orders;
using GSP.Domain.Params;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace GSP.DAL.Repositories
{
    public class OrderRepository : GameStoreRepository<Order>, IOrderRepository
    {
        private readonly GameStoreContext _dbContext;

        public OrderRepository(GameStoreContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Order> GetOrdersByParams(OrdersFilterParams filterParams, out int totalCount)
        {
            var query = _dbContext.Orders
                .Include(x => x.Customer)
                .Include(x => x.Games)
                .ThenInclude(x => x.Game)
                .ThenInclude(x => x.Category).AsQueryable();

            FillOrdersQueryFilterParams(filterParams);
            query = query.Where(filterParams.Expression).OrderByDescending(t => t.OrderId);

            totalCount = query.Count();

            return query
                .Skip(filterParams.PageSize * (filterParams.PageNumber - 1))
                .Take(filterParams.PageSize)
                .OrderByDescending(x => x.OrderId)
                .AsEnumerable();
        }
        
        public Order GetCurrentCustomerOrder(int customerId)
        {
            return _dbContext.Orders
                .Include(x => x.Games)
                .ThenInclude(x => x.Game)
                .ThenInclude(x => x.Category)
                .FirstOrDefault(x => x.Status == OrderStatus.New && x.CustomerId == customerId);
        }

        public void AddGameToBucket(OrderGame game)
        {
            _dbContext.OrderGames.Add(game);
        }

        public void DeleteGameFromBucket(int id)
        {
            var game = _dbContext.OrderGames.Find(id);
            _dbContext.OrderGames.Remove(game);
        }

        private void FillOrdersQueryFilterParams(OrdersFilterParams filterParams)
        {
            var predicate = PredicateBuilder.New<Order>(x => x.Status == OrderStatus.Complete);

            if (filterParams.CustomerId.HasValue)
            {
                predicate = predicate.Extend(x => x.CustomerId == filterParams.CustomerId, PredicateOperator.And);
            }

            filterParams.Expression = predicate;
        }
    }
}
