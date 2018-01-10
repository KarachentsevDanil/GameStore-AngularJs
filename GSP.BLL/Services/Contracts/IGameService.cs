using System.Collections.Generic;
using GSP.Domain.Games;
using GSP.Domain.Params;

namespace GSP.BLL.Services.Contracts
{
    public interface IGameService
    {
        void AddGame(Game game);

        void UpdateGame(Game game);

        IEnumerable<Game> GetGames();

        IEnumerable<Game> GetGamesByParams(FilterParams<Game> gameParams, out int totalCount);

        Game GetGameById(int gameId);

        void DeleteGame(int gameId);
    }
}
