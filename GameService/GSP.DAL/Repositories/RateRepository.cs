using System.Collections.Generic;
using System.Linq;
using GSP.Common.DAL.Repositories;
using GSP.Games.DAL.Context;
using GSP.Games.DAL.Repositories.Contracts;
using GSP.Games.Domain.Games;

namespace GSP.Games.DAL.Repositories
{
    public class RateRepository : GameStoreRepository<Rate, GameStoreGameContext>, IRateRepository
    {
        private readonly GameStoreGameContext _dbContext;

        public RateRepository(GameStoreGameContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<Rate> GetRatesOfGame(int gameId)
        {
            return _dbContext.Rates
                .Where(x => x.GameId == gameId)
                .ToList();
        }
    }
}
