using System.Collections.Generic;
using GSP.Games.BLL.Dto.Rate;
using GSP.Games.BLL.Services.Contracts;
using GSP.Games.DAL.UnitOfWork.Contracts;
using GSP.Games.Domain.Games;

namespace GSP.Games.BLL.Services
{
    public class RateService : IRateService
    {
        private readonly IGameStoreGameUnitOfWork _unitOfWork;

        public RateService(IGameStoreGameUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddFeedbackToGame(CreateRateDto rate)
        {
            var newRate = AutoMapper.Mapper.Map<CreateRateDto, Rate>(rate);
            _unitOfWork.RateRepository.Add(newRate);
            _unitOfWork.Commit();
        }

        public IEnumerable<RateDto> GetRatesOfGame(int gameId)
        {
            var rates = _unitOfWork.RateRepository.GetRatesOfGame(gameId);
            return AutoMapper.Mapper.Map<IEnumerable<Rate>, List<RateDto>>(rates);
        }
    }
}
