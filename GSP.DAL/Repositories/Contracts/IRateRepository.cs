using System.Collections.Generic;
using GSP.Domain.Games;
using GSP.Domain.Params;

namespace GSP.DAL.Repositories.Contracts
{
    public interface IRateRepository : IGameStoreRepository<Rate>
    {
        IEnumerable<Rate> GetRatesOfGame(int gameId);

        IEnumerable<Game> GetTopRateGames(FilterParams<Game> filterParams);

        IEnumerable<Game> GetTopSellGames(FilterParams<Game> filterParams);
    }
}
