using GSP.DAL.EF.Context;
using GSP.DAL.EF.Repositories;
using GSP.Domain.Games;
using GSP.Rate.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Rate.DAL.EF.Repositories
{
    public class RateRepository : BaseRepository<int, RateBase, RateContext>, IRateRepository
    {
        private readonly RateContext _dbContext;

        public RateRepository(RateContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<RateBase>> GetRatesOfGameAsync(int gameId, CancellationToken ct = default)
        {
            return await _dbContext.Rates
                .Where(x => x.GameId == gameId)
                .ToListAsync(ct);
        }
    }
}