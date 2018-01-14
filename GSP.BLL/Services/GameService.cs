using System.Collections.Generic;
using System.Linq;
using GSP.AprioriAlgoritm.Contracts;
using GSP.BLL.Services.Contracts;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Domain.Games;
using GSP.Domain.Params;
using Microsoft.EntityFrameworkCore;

namespace GSP.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IGameStoreUnitOfWork _unitOfWork;
        private readonly IRecomendationService _recomendationService;

        public GameService(IGameStoreUnitOfWork unitOfWork, IRecomendationService recomendationService)
        {
            _unitOfWork = unitOfWork;
            _recomendationService = recomendationService;
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

        public IEnumerable<Game> GetGamesByParams(GamesFilterParams gameParams, out int totalCount)
        {
            return _unitOfWork.GameRepository.GetGamesByParams(gameParams, out totalCount);
        }

        public Game GetGameById(int gameId)
        {
            return _unitOfWork.GameRepository.GetGameById(gameId);
        }

        public void DeleteGame(int gameId)
        {
            _unitOfWork.GameRepository.Delete(gameId);
            _unitOfWork.Commit();
        }

        public IEnumerable<Game> GetRecomendedGames(int gameId)
        {
            var totalTransactionsCount = _unitOfWork.OrderRepository.GetAll().Count(t => t.Status == Domain.Orders.OrderStatus.Complete);
            var gameTransactions = _unitOfWork.OrderRepository.GetAll()
                .Include(t => t.Games)
                .Where(x => x.Games.Any(t => t.GameId == gameId))
                .Select(t => t.Games.Select(g => g.GameId).ToArray())
                .ToList();

            var recomendedGamesIds = _recomendationService.GetRecomendations(gameTransactions, totalTransactionsCount, gameId);
            var games = _unitOfWork.GameRepository.GetGamesByIds(recomendedGamesIds.ToArray());

            return games;
        }
    }
}
