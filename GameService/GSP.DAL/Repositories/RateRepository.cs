using System.Collections.Generic;
using System.Linq;
using GSP.Common.DAL.Repositories;
using GSP.DAL.Context;
using GSP.Domain.Games;
using GSP.Game.DAL.Context;
using GSP.Game.DAL.Repositories.Contracts;
using GSP.Games.Domain.Games;
using Microsoft.EntityFrameworkCore;

namespace GSP.DAL.Repositories
{
    public class RateRepository : GameStoreRepository<Rate, GameStoreGameContext>, IRateRepository
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
    }
}
