using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public IEnumerable<Game> GetItems(Expression<Func<Game, bool>> expression)
        {
            return _dbContext.Games.Where(expression.Compile()).AsEnumerable();
        }

        public IEnumerable<Game> GetItemsByParams(FilterParams<Game> filterParams)
        {
            return _dbContext.Games
                .Where(filterParams.Expression.Compile())
                .Take(filterParams.PageSize)
                .Skip(filterParams.PageSize * (filterParams.PageNumber - 1))
                .OrderByDescending(x => x.GameId)
                .AsEnumerable();
        }

        public IEnumerable<Game> GetGames()
        {
            return _dbContext.Games
                .Include(x=> x.Category)
                .AsEnumerable();
        }

        public IEnumerable<Game> GetGamesByCategory(int categoryId)
        {
            return _dbContext.Games.Where(x => x.CategoryId == categoryId)
                .Include(x=> x.Category)
                .AsEnumerable();
        }

        public IEnumerable<Game> GetGamesByTerm(string term)
        {
            return _dbContext.Games
                .Include(x=> x.Category)
                .Where(x => x.Name.ToLower() == term.ToLower()).AsEnumerable();
        }
    }
}
