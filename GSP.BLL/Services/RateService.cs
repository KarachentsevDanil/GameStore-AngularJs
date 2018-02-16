using System.Collections.Generic;
using GSP.BLL.Dto.Rate;
using GSP.BLL.Services.Contracts;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Domain.Games;

namespace GSP.BLL.Services
{
    public class RateService : IRateService
    {
        private readonly IGameStoreUnitOfWork _unitOfWork;

        public RateService(IGameStoreUnitOfWork unitOfWork)
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
