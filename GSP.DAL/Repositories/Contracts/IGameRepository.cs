using System.Collections.Generic;
using GSP.Domain.Games;

namespace GSP.DAL.Repositories.Contracts
{
    public interface IGameRepository : IGameStoreRepository<Game>, IExperssionable<Game>
    {
        IEnumerable<Game> GetGames();

        IEnumerable<Game> GetGamesByCategory(int categoryId);

        IEnumerable<Game> GetGamesByTerm(string term);
    }
}
