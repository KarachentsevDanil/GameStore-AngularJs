using GSP.DAL.Repositories.Contracts;
using GSP.Domain.Params;
using GSP.Game.Domain.Games;
using GSP.Game.Domain.Params;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Game.DAL.Repositories.Contracts
{
    public interface IGameRepository : IGenericRepository<int, GameBase>
    {
        Task<IEnumerable<GameBase>> GetGamesByIdsAsync(int[] ids, CancellationToken ct = default);

        Task<GameBase> GetGameByIdAsync(int id, CancellationToken ct = default);

        Task<CollectionResult<GameBase>> GetGamesByParamsAsync(GamesFilterParams filterParams, CancellationToken ct = default);
    }
}