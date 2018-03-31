using System.Collections.Generic;
using System.Linq;
using GSP.Common.DAL.Repositories;
using GSP.Orders.DAL.Context;
using GSP.Orders.DAL.Repositories.Contracts;
using GSP.Orders.Domain.Orders;
using GSP.Orders.Domain.Params;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace GSP.Orders.DAL.Repositories
{
    public class OrderRepository : GameStoreRepository<Order, GameStoreOrderContext>, IOrderRepository
    {
        private readonly GameStoreOrderContext _dbContext;

        public OrderRepository(GameStoreOrderContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Order> GetOrdersByParams(OrdersFilterParams filterParams, out int totalCount)
        {
            var query = GetAllOrders();

            FillOrdersQueryFilterParams(filterParams);
            query = query.Where(filterParams.Expression).OrderByDescending(t => t.OrderId);

            totalCount = query.Count();

            return query
                .Skip(filterParams.PageSize * (filterParams.PageNumber - 1))
                .Take(filterParams.PageSize)
                .OrderByDescending(x => x.OrderId)
                .AsEnumerable();
        }

        public Order GetCurrentCustomerOrder(string customerId)
        {
            return GetAllOrders().FirstOrDefault(x => x.Status == OrderStatus.New && x.CustomerId == customerId);
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

            if (!string.IsNullOrEmpty(filterParams.CustomerId))
            {
                predicate = predicate.Extend(x => x.CustomerId == filterParams.CustomerId, PredicateOperator.And);
            }

            filterParams.Expression = predicate;
        }

        private IQueryable<Order> GetAllOrders()
        {
            return _dbContext.Orders
                .Include(x => x.Payment)
                .Include(x => x.Games)
                .Where(x => !x.IsDeleted).AsQueryable();
        }
    }
}
