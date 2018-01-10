using System.Collections.Generic;
using System.Linq;
using GSP.DAL.Context;
using GSP.DAL.Repositories.Contracts;
using GSP.Domain.Games;
using GSP.Domain.Params;
using Microsoft.EntityFrameworkCore;

namespace GSP.DAL.Repositories
{
    public class RateRepository : GameStoreRepository<Rate>, IRateRepository
    {
        private readonly GameStoreContext _dbContext;

        public RateRepository(GameStoreContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<Rate> GetRatesOfGame(int gameId)
        {
            return _dbContext.Rates
                .Include(t=> t.Customer)
                .Where(x => x.GameId == gameId)
                .ToList();
        }

        public IEnumerable<Game> GetTopRateGames(FilterParams<Game> filterParams)
        {
            return _dbContext.Games
                .Include(x => x.Category)
                .Where(filterParams.Expression.Compile())
                .Take(filterParams.PageSize)
                .Skip(filterParams.PageSize * (filterParams.PageNumber - 1))
                .OrderByDescending(x => x.Rates.Sum(p => (int)p.Rating))
                .ThenBy(x => x.Name)
                .AsEnumerable();
        }

        public IEnumerable<Game> GetTopSellGames(FilterParams<Game> filterParams)
        {
            return _dbContext.Games
                .Include(x => x.Category)
                .Where(filterParams.Expression.Compile())
                .Take(filterParams.PageSize)
                .Skip(filterParams.PageSize * (filterParams.PageNumber - 1))
                .OrderByDescending(x => x.Orders.Count)
                .ThenBy(x => x.Name)
                .AsEnumerable();
        }
    }
}
