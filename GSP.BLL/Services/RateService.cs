using System.Collections.Generic;
using GSP.BLL.Services.Contracts;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Domain.Games;
using GSP.Domain.Params;

namespace GSP.BLL.Services
{
    public class RateService : IRateService
    {
        private readonly IGameStoreUnitOfWork _unitOfWork;

        public RateService(IGameStoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddFeedbackToGame(Rate rate)
        {
            _unitOfWork.RateRepository.Add(rate);
            _unitOfWork.Commit();
        }

        public IEnumerable<Rate> GetRatesOfGame(int gameId)
        {
            return _unitOfWork.RateRepository.GetRatesOfGame(gameId);
        }

        public IEnumerable<Game> GetTopRateGames(FilterParams<Game> filterParams)
        {
            return _unitOfWork.RateRepository.GetTopRateGames(filterParams);
        }

        public IEnumerable<Game> GetTopSellGames(FilterParams<Game> filterParams)
        {
            return _unitOfWork.RateRepository.GetTopSellGames(filterParams);
        }
    }
}
