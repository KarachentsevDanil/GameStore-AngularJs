using System.Collections.Generic;
using GSP.Domain.Games;

namespace GSP.DAL.Repositories.Contracts
{
    public interface IRateRepository : IGameStoreRepository<Rate>
    {
        IEnumerable<Rate> GetRatesOfGame(int gameId);
    }
}
