using GSP.DAL.Repositories.Contracts;
using GSP.Domain.Games;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Rate.DAL.Repositories.Contracts
{
    public interface IRateRepository : IGenericRepository<int, RateBase>
    {
        Task<IEnumerable<RateBase>> GetRatesOfGameAsync(int gameId, CancellationToken ct = default);
    }
}