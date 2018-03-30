using System.Collections.Generic;
using GSP.BLL.Dto.Game;
using GSP.Games.Domain.Params;

namespace GSP.BLL.Services.Contracts
{
    public interface IGameService
    {
        void AddGame(CreateGameDto game);

        void UpdateGame(GameDto game);

        IEnumerable<GameDto> GetRecomendedGames(int gameId);

        IEnumerable<GameDto> GetCustomerGames(string customerName);

        IEnumerable<GameDto> GetGamesByParams(GamesFilterParams gameParams, out int totalCount);

        GameDto GetGameById(int gameId);

        void DeleteGame(int gameId);
    }
}
