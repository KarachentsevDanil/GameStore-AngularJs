using AutoMapper;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Domain.Games;
using GSP.Rate.BLL.DTOs.Rate;
using GSP.Rate.BLL.Services.Contracts;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Rate.BLL.Services
{
    public class RateService : IRateService
    {
        private readonly IRateUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<RateService> _logger;

        public RateService(
            IRateUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<RateService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddFeedbackToGameAsync(CreateRateDto rate, CancellationToken ct = default)
        {
            var newRate = _mapper.Map<RateBase>(rate);

            _unitOfWork.RateRepository.Create(newRate);

            await _unitOfWork.CommitAsync(ct);
        }

        public async Task<IEnumerable<RateDto>> GetRatesOfGameAsync(int gameId, CancellationToken ct = default)
        {
            var rates = await _unitOfWork.RateRepository.GetRatesOfGameAsync(gameId, ct);
            var ratesListDto = _mapper.Map<List<RateDto>>(rates);

            return ratesListDto;
        }
    }
}