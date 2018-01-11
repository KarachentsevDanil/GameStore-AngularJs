using System;
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
    public class GameRepository : GameStoreRepository<Game>, IGameRepository
    {
        private readonly GameStoreContext _dbContext;

        public GameRepository(GameStoreContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Game> GetGamesByParams(GamesFilterParams filterParams, out int totalCount)
        {
            var query = _dbContext.Games
                .Include(x => x.Category)
                .Include(x => x.Orders)
                .ThenInclude(x => x.Order)
                .Include(x => x.Rates)
                .AsQueryable();

            FillGameQueryFilterParams(filterParams);
            query = FillSortParams(query, filterParams);

            totalCount = query.Count();

            var result = query
                    .Skip(filterParams.PageSize * (filterParams.PageNumber - 1))
                    .Take(filterParams.PageSize == 0 ? int.MaxValue : filterParams.PageSize)
                    .AsNoTracking()
                    .ToList();

            return result;
        }

        public IEnumerable<Game> GetGames()
        {
            return _dbContext.Games
                .Include(x => x.Category)
                .ToList();
        }

        private void FillGameQueryFilterParams(GamesFilterParams filterParams)
        {
            var predicate = PredicateBuilder.New<Game>(x => !x.IsDeleted);

            if (filterParams.CategoriesIds != null && filterParams.CategoriesIds.Any())
            {
                predicate = predicate.Extend(p => filterParams.CategoriesIds.Contains(p.CategoryId), PredicateOperator.And);
            }
            ;
            if (!string.IsNullOrEmpty(filterParams.Term))
            {
                predicate = predicate.Extend(x => x.Name.ToLower().Contains(filterParams.Term.ToLower()), PredicateOperator.And);
            }

            if (filterParams.StartPrice.HasValue && filterParams.EndPrice.HasValue)
            {
                predicate = predicate.Extend(x => filterParams.StartPrice <= x.Price && filterParams.EndPrice >= x.Price, PredicateOperator.And);
            }

            if (filterParams.CustomerId.HasValue)
            {
                predicate = predicate.Extend(x => x.Orders.Any(o => o.Order.CustomerId == filterParams.CustomerId && o.Order.Status == OrderStatus.Complete), PredicateOperator.And);
            }

            filterParams.Expression = predicate;
        }

        private IQueryable<Game> FillSortParams(IQueryable<Game> games, GamesFilterParams gamesFilterParams)
        {
            games = games.Where(gamesFilterParams.Expression);

            switch (gamesFilterParams.OutputMode)
            {
                case GamesOutputMode.All:
                    games = games.OrderBy(x => x.Name).AsQueryable();
                    break;
                case GamesOutputMode.TopSell:
                    games = games.OrderBy(x => x.Orders.Count).AsQueryable();
                    break;
                case GamesOutputMode.TopRate:
                    games = games.OrderBy(x => x.Rates.Sum(t => (int)t.Rating)).AsQueryable();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gamesFilterParams.OutputMode), gamesFilterParams.OutputMode, "Wrong option was sent.");
            }

            return games;
        }
    }
}
