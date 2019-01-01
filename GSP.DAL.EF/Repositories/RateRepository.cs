using GSP.DAL.EF.Context;
using GSP.DAL.Repositories.Contracts;
using GSP.Domain.Games;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GSP.DAL.EF.Repositories
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
    }
}
