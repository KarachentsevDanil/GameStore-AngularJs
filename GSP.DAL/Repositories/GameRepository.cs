using System.Collections.Generic;
using System.Linq;
using GSP.DAL.Context;
using GSP.DAL.Repositories.Contracts;
using GSP.Domain.Games;
using GSP.Domain.Params;
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
        
        public IEnumerable<Game> GetGamesByParams(FilterParams<Game> filterParams, out int totalCount)
        {
            var query = _dbContext.Games
                .Include(x => x.Category)
                .Where(filterParams.Expression.Compile())
                .OrderBy(x => x.Name)
                .AsQueryable();

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
    }
}
