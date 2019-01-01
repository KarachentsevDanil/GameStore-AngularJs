using GSP.Domain.Params;
using GSP.Game.BLL.DTOs.Game;
using GSP.Game.Domain.Params;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Game.BLL.Services.Contracts
{
    public interface IGameService
    {
        Task AddGameAsync(CreateGameDto game, CancellationToken ct = default);

        Task UpdateGameAsync(GameDto game, CancellationToken ct = default);

        Task<CollectionResult<GameDto>> GetGamesByParamsAsync(GamesFilterParams gameParams, CancellationToken ct = default);

        Task<GameDto> GetGameByIdAsync(int gameId, CancellationToken ct = default);

        Task DeleteGameAsync(int gameId, CancellationToken ct = default);
    }
}