using AutoMapper;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Domain.Params;
using GSP.Game.BLL.DTOs.Game;
using GSP.Game.BLL.Services.Contracts;
using GSP.Game.Domain.Games;
using GSP.Game.Domain.Params;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Game.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IGameUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<GameService> _logger;

        public GameService(
            IGameUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<GameService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddGameAsync(CreateGameDto game, CancellationToken ct = default)
        {
            var newGame = _mapper.Map<GameBase>(game);
            _unitOfWork.GameRepository.Create(newGame);

            await _unitOfWork.CommitAsync(ct);
        }

        public async Task UpdateGameAsync(GameDto game, CancellationToken ct = default)
        {
            var updateGame = _mapper.Map<GameBase>(game);
            _unitOfWork.GameRepository.Update(updateGame);

            await _unitOfWork.CommitAsync(ct);
        }

        public async Task<CollectionResult<GameDto>> GetGamesByParamsAsync(GamesFilterParams gameParams, CancellationToken ct = default)
        {
            var games = await _unitOfWork.GameRepository.GetGamesByParamsAsync(gameParams, ct);
            var gameListDto =  _mapper.Map<CollectionResult<GameDto>>(games);

            return gameListDto;
        }

        public async Task<GameDto> GetGameByIdAsync(int gameId, CancellationToken ct = default)
        {
            var game = await _unitOfWork.GameRepository.GetGameByIdAsync(gameId, ct);
            var gameDto = _mapper.Map<GameDto>(game);

            return gameDto;
        }

        public async Task DeleteGameAsync(int gameId, CancellationToken ct = default)
        {
            _unitOfWork.GameRepository.Delete(gameId);
            await _unitOfWork.CommitAsync(ct);
        }
    }
}