using System.Collections.Generic;
using GSP.Common.DAL.Repositories.Contracts;
using GSP.Games.Domain.Games;

namespace GSP.Game.DAL.Repositories.Contracts
{
    public interface IRateRepository : IGameStoreRepository<Rate>
    {
        IEnumerable<Rate> GetRatesOfGame(int gameId);
    }
}
