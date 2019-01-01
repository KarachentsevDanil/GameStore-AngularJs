using GSP.DAL.EF.Context;
using GSP.DAL.EF.Repositories;
using GSP.Domain.Params;
using GSP.Game.DAL.Repositories.Contracts;
using GSP.Game.Domain.Enums;
using GSP.Game.Domain.Games;
using GSP.Game.Domain.Params;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Game.DAL.EF.Repositories
{
    public class GameRepository : BaseRepository<int, GameBase, GameContext>, IGameRepository
    {
        private readonly GameContext _dbContext;

        public GameRepository(GameContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CollectionResult<GameBase>> GetGamesByParamsAsync(GamesFilterParams filterParams, CancellationToken ct = default)
        {
            var collectionResult = new CollectionResult<GameBase>();

            var query = GetAllGames();

            FillGamesQueryFilterParams(filterParams);
            query = FillSortParams(query, filterParams);

            collectionResult.TotalCount = query.Count();

            collectionResult.Collection = await query
                    .Skip(filterParams.PageSize * (filterParams.PageNumber - 1))
                    .Take(filterParams.PageSize == 0 ? int.MaxValue : filterParams.PageSize)
                    .AsNoTracking()
                    .ToListAsync(ct);

            return collectionResult;
        }

        public async Task<IEnumerable<GameBase>> GetGamesByIdsAsync(int[] ids, CancellationToken ct = default)
        {
            return await GetAllGames()
                .Where(t => ids.Contains(t.GameId))
                .ToListAsync(ct);
        }

        public async Task<GameBase> GetGameByIdAsync(int id, CancellationToken ct = default)
        {
            return await GetAllGames()
                .FirstOrDefaultAsync(t => id == t.GameId, ct);
        }

        private void FillGamesQueryFilterParams(GamesFilterParams filterParams)
        {
            var predicate = PredicateBuilder.New<GameBase>(x => !x.IsDeleted);

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

            filterParams.Expression = predicate;
        }

        private IQueryable<GameBase> FillSortParams(IQueryable<GameBase> games, GamesFilterParams gamesFilterParams)
        {
            games = games.Where(gamesFilterParams.Expression);

            switch (gamesFilterParams.SortMode)
            {
                case GameSortMode.ByName:
                    games = games.OrderBy(x => x.Name).AsQueryable();
                    break;
                case GameSortMode.ByCountOfSales:
                    games = games.OrderByDescending(x => x.OrdersCount).AsQueryable();
                    break;
                case GameSortMode.ByRating:
                    games = games.OrderByDescending(x => x.AverageRate).AsQueryable();
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

        private IQueryable<GameBase> GetAllGames()
        {
            return _dbContext.Games
                .Include(x => x.Category)
                .Include(x => x.Photos)
                .Where(x => !x.IsDeleted);
        }
    }
}