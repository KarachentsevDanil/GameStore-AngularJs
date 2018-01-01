using System.Collections.Generic;
using GSP.Domain.Games;

namespace GSP.BLL.Services.Contracts
{
    public interface IGameService
    {
        void AddGame(Game game);

        void UpdateGame(Game game);

        IEnumerable<Game> GetGamesByCategory(int categoryId);

        IEnumerable<Game> GetGamesByTerm(string term);

        IEnumerable<Game> GetGames();

        Game GetGameById(int gameId);

        void DeleteGame(int gameId);
    }
}
