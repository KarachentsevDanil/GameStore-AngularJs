using System.Collections.Generic;
using GSP.BLL.Dto.Game;
using GSP.Domain.Params;

namespace GSP.BLL.Services.Contracts
{
    public interface IGameService
    {
        void AddGame(CreateGameDto game);

        void UpdateGame(CreateGameDto game);

        IEnumerable<GameDto> GetRecomendedGames(int gameId);

        IEnumerable<GameDto> GetGamesByParams(GamesFilterParams gameParams, out int totalCount);

        GameDto GetGameById(int gameId);

        void DeleteGame(int gameId);
    }
}
