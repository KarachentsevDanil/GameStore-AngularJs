using System.Collections.Generic;
using GSP.Domain.Games;
using GSP.Domain.Params;

namespace GSP.DAL.Repositories.Contracts
{
    public interface IGameRepository : IGameStoreRepository<Game>
    {
        IEnumerable<Game> GetGamesByIds(int[] ids);

        Game GetGameById(int id);

        IEnumerable<Game> GetCustomerGames(string customerId);

        IEnumerable<Game> GetGamesByParams(GamesFilterParams filterParams, out int totalCount);
    }
}
