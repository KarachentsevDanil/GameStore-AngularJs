using System.Collections.Generic;
using System.Linq;
using GSP.AprioriAlgoritm.Contracts;
using GSP.BLL.Dto.Game;
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

        public IEnumerable<GameDto> GetRecomendedGames(int gameId)
        {
            var totalTransactionsCount = _unitOfWork.OrderRepository
                .GetAll().Count(t => t.Status == Domain.Orders.OrderStatus.Complete);

            var gameTransactions = _unitOfWork.OrderRepository.GetAll()
                .Include(t => t.Games)
                .Where(x => x.Games.Any(t => t.GameId == gameId))
                .Select(t => t.Games.Select(g => g.GameId).ToArray())
                .ToList();

            if (!gameTransactions.Any())
            {
                return Enumerable.Empty<GameDto>();
            }

            var recomendedGamesIds = _recomendationService.GetRecomendations(gameTransactions, totalTransactionsCount, gameId);
            var games = _unitOfWork.GameRepository.GetGamesByIds(recomendedGamesIds.ToArray());

            return AutoMapper.Mapper.Map<IEnumerable<Game>, List<GameDto>>(games);
        }

        public IEnumerable<GameDto> GetCustomerGames(string customerName)
        {
            var games = _unitOfWork.GameRepository.GetCustomerGames(customerName);
            return AutoMapper.Mapper.Map<IEnumerable<Game>, List<GameDto>>(games);
        }
    }
}
