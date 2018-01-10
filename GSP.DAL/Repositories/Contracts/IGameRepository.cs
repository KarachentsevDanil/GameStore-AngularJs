using System.Collections.Generic;
using GSP.Domain.Games;
using GSP.Domain.Params;

namespace GSP.DAL.Repositories.Contracts
{
    public interface IGameRepository : IGameStoreRepository<Game>
    {
        IEnumerable<Game> GetGames();

        IEnumerable<Game> GetGamesByParams(FilterParams<Game> filterParams, out int totalCount);
    }
}
