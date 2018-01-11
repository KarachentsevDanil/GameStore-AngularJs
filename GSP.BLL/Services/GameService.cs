using System.Collections.Generic;
using System.Linq;
using GSP.BLL.Services.Contracts;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Domain.Games;
using GSP.Domain.Params;

namespace GSP.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IGameStoreUnitOfWork _unitOfWork;

        public GameService(IGameStoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddGame(Game game)
        {
            _unitOfWork.GameRepository.Add(game);
            _unitOfWork.Commit();
        }

        public void UpdateGame(Game game)
        {
            _unitOfWork.GameRepository.Update(game);
            _unitOfWork.Commit();
        }

        public IEnumerable<Game> GetGames()
        {
            return _unitOfWork.GameRepository.GetGames();
        }

        public IEnumerable<Game> GetGamesByParams(GamesFilterParams gameParams, out int totalCount)
        {
            return _unitOfWork.GameRepository.GetGamesByParams(gameParams, out totalCount);
        }

        public Game GetGameById(int gameId)
        {
            return _unitOfWork.GameRepository.GetGames().FirstOrDefault(x=> x.GameId == gameId);
        }

        public void DeleteGame(int gameId)
        {
            _unitOfWork.GameRepository.Delete(gameId);
            _unitOfWork.Commit();
        }
    }
}
