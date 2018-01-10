using System.Collections.Generic;
using System.Linq;
using GSP.DAL.Context;
using GSP.DAL.Repositories.Contracts;
using GSP.Domain.Orders;
using GSP.Domain.Params;
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
        
        public IEnumerable<Order> GetOrdersByParams(FilterParams<Order> filterParams, out int totalCount)
        {
            var query = _dbContext.Orders
                .Include(x => x.Customer)
                .Include(x => x.Games)
                .ThenInclude(x => x.Game)
                .ThenInclude(x => x.Category)
                .Where(filterParams.Expression.Compile()).AsQueryable();

            totalCount = query.Count();

            return query
                .Skip(filterParams.PageSize * (filterParams.PageNumber - 1))
                .Take(filterParams.PageSize)
                .OrderByDescending(x => x.OrderId)
                .AsEnumerable();
        }

        public IEnumerable<Order> GetOrders()
        {
            return _dbContext.Orders
                .Include(x => x.Customer)
                .Include(x => x.Games)
                .ThenInclude(x => x.Game)
                .ThenInclude(x => x.Category)
                .Where(x => x.Games.Any())
                .AsEnumerable();
        }

        public IEnumerable<Order> GetCustomerOrders(int customerId)
        {
            return _dbContext.Orders
                .Include(x => x.Customer)
                .Include(x => x.Games)
                .ThenInclude(x => x.Game)
                .ThenInclude(x => x.Category)
                .Where(x => x.CustomerId == customerId && x.Games.Any())
                .AsEnumerable();
        }

        public Order GetCurrentCustomerOrder(int customerId)
        {
            return _dbContext.Orders
                .Include(x => x.Games)
                .ThenInclude(x=> x.Game)
                .ThenInclude(x=> x.Category)
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
    }
}
