using System.Collections.Generic;
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

        public IEnumerable<Game> GetGamesByCategory(int categoryId)
        {
            return _unitOfWork.GameRepository.GetGamesByCategory(categoryId);
        }

        public IEnumerable<Game> GetGamesByTerm(string term)
        {
            return _unitOfWork.GameRepository.GetGamesByTerm(term);
        }

        public IEnumerable<Game> GetGames()
        {
            return _unitOfWork.GameRepository.GetGames();
        }

        public IEnumerable<Game> GetGamesByParams(FilterParams<Game> gameParams, out int totalCount)
        {
            return _unitOfWork.GameRepository.GetItemsByParams(gameParams, out totalCount);
        }

        public Game GetGameById(int gameId)
        {
            return _unitOfWork.GameRepository.GetById(gameId);
        }

        public void DeleteGame(int gameId)
        {
            _unitOfWork.GameRepository.Delete(gameId);
            _unitOfWork.Commit();
        }
    }
}
