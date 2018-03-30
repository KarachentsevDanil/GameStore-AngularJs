using System.Collections.Generic;
using GSP.Common.DAL.Repositories.Contracts;
using GSP.Games.Domain.Params;

namespace GSP.Game.DAL.Repositories.Contracts
{
    public interface IGameRepository : IGameStoreRepository<Games.Domain.Games.Game>
    {
        IEnumerable<Games.Domain.Games.Game> GetGamesByIds(int[] ids);

        Games.Domain.Games.Game GetGameById(int id);

        IEnumerable<Games.Domain.Games.Game> GetCustomerGames(string customerName);

        IEnumerable<Games.Domain.Games.Game> GetGamesByParams(GamesFilterParams filterParams, out int totalCount);
    }
}
