using System.Collections.Generic;
using GSP.Games.BLL.Dto.Game;
using GSP.Games.Domain.Params;

namespace GSP.Games.BLL.Services.Contracts
{
    public interface IGameService
    {
        void AddGame(CreateGameDto game);

        void UpdateGame(GameDto game);

        IEnumerable<GameDto> GetGamesByParams(GamesFilterParams gameParams, out int totalCount);

        GameDto GetGameById(int gameId);

        void DeleteGame(int gameId);
    }
}
