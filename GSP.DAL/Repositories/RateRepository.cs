using System.Collections.Generic;
using System.Linq;
using GSP.DAL.Context;
using GSP.DAL.Repositories.Contracts;
using GSP.Domain.Games;

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
            return _dbContext.Rates.Where(x => x.GameId == gameId).AsEnumerable();
        }
    }
}
