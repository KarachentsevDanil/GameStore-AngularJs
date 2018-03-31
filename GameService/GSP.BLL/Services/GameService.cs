using System.Collections.Generic;
using GSP.Games.BLL.Dto.Game;
using GSP.Games.BLL.Services.Contracts;
using GSP.Games.DAL.UnitOfWork.Contracts;
using GSP.Games.Domain.Games;
using GSP.Games.Domain.Params;

namespace GSP.Games.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IGameStoreGameUnitOfWork _unitOfWork;

        public GameService(IGameStoreGameUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddGame(CreateGameDto game)
        {
            var newGame = AutoMapper.Mapper.Map<CreateGameDto, Game>(game);
            _unitOfWork.GameRepository.Add(newGame);
            _unitOfWork.Commit();
        }

        public void UpdateGame(GameDto game)
        {
            var updateGame = AutoMapper.Mapper.Map<GameDto, Game>(game);
            _unitOfWork.GameRepository.Update(updateGame);
            _unitOfWork.Commit();
        }

        public IEnumerable<GameDto> GetGamesByParams(GamesFilterParams gameParams, out int totalCount)
        {
            var games = _unitOfWork.GameRepository.GetGamesByParams(gameParams, out totalCount);
            return AutoMapper.Mapper.Map<IEnumerable<Game>, List<GameDto>>(games);
        }

        public GameDto GetGameById(int gameId)
        {
            var game = _unitOfWork.GameRepository.GetGameById(gameId);
            return AutoMapper.Mapper.Map<Game, GameDto>(game);
        }

        public void DeleteGame(int gameId)
        {
            _unitOfWork.GameRepository.Delete(gameId);
            _unitOfWork.Commit();
        }
    }
}
