using System;
using System.Collections.Generic;
using System.Linq;
using GSP.Common.DAL.Repositories;
using GSP.Game.DAL.Context;
using GSP.Game.DAL.Repositories.Contracts;
using GSP.Games.Domain.Params;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace GSP.Game.DAL.Repositories
{
    public class GameRepository : GameStoreRepository<Games.Domain.Games.Game, GameStoreGameContext>, IGameRepository
    {
        private readonly GameStoreGameContext _dbContext;

        public GameRepository(GameStoreGameContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Games.Domain.Games.Game> GetGamesByParams(GamesFilterParams filterParams, out int totalCount)
        {
            var query = GetAllGames();

            FillGamesQueryFilterParams(filterParams);
            query = FillSortParams(query, filterParams);

            totalCount = query.Count();

            var result = query
                    .Skip(filterParams.PageSize * (filterParams.PageNumber - 1))
                    .Take(filterParams.PageSize == 0 ? int.MaxValue : filterParams.PageSize)
                    .AsNoTracking()
                    .ToList();

            return result;
        }

        private void FillGamesQueryFilterParams(GamesFilterParams filterParams)
        {
            var predicate = PredicateBuilder.New<Games.Domain.Games.Game>(x => !x.IsDeleted);

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

            if (!string.IsNullOrEmpty(filterParams.CustomerId))
            {
                predicate = predicate.Extend(x => x.Orders.Any(o => o.Order.CustomerId == filterParams.CustomerId && o.Order.Status == OrderStatus.Complete), PredicateOperator.And);
            }

            filterParams.Expression = predicate;
        }

        private IQueryable<Games.Domain.Games.Game> FillSortParams(IQueryable<Games.Domain.Games.Game> games, GamesFilterParams gamesFilterParams)
        {
            games = games.Where(gamesFilterParams.Expression);

            switch (gamesFilterParams.SortMode)
            {
                case GameSortMode.ByName:
                    games = games.OrderBy(x => x.Name).AsQueryable();
                    break;
                case GameSortMode.ByCountOfSales:
                    games = games.OrderByDescending(x => x.Orders.Count).AsQueryable();
                    break;
                case GameSortMode.ByRating:
                    games = games.OrderByDescending(x => x.Rates.Sum(t => t.Rating)).AsQueryable();
                    break;
                case GameSortMode.ByPriceHighToLow:
                    games = games.OrderByDescending(x => x.Price).AsQueryable();
                    break;
                case GameSortMode.ByPriceLowToHigh:
                    games = games.OrderBy(x => x.Price).AsQueryable();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gamesFilterParams.SortMode), gamesFilterParams.SortMode, "Wrong option was sent.");
            }

            return games;
        }

        public IEnumerable<Games.Domain.Games.Game> GetGamesByIds(int[] ids)
        {
            return GetAllGames()
                .Where(t => ids.Contains(t.GameId))
                .ToList();
        }

        public Games.Domain.Games.Game GetGameById(int id)
        {
            return GetAllGames()
                .FirstOrDefault(t => id == t.GameId);
        }

        public IEnumerable<Games.Domain.Games.Game> GetCustomerGames(string customerName)
        {
            return GetAllGames().Where(g => g.Orders.Any(t => t.Order.Customer.Email.ToUpper() == customerName.ToUpper())).ToList();
        }

        private IQueryable<Games.Domain.Games.Game> GetAllGames()
        {
            return _dbContext.Games
                .Include(x => x.Category)
                .Include(x => x.Rates)
                .Include(x => x.Photos)
                .Where(x => !x.IsDeleted);
        }
    }
}
