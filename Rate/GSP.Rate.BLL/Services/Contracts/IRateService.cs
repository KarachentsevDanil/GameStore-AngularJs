using GSP.Rate.BLL.DTOs.Rate;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Rate.BLL.Services.Contracts
{
    public interface IRateService
    {
        Task AddFeedbackToGameAsync(CreateRateDto rate, CancellationToken ct = default);

        Task<IEnumerable<RateDto>> GetRatesOfGameAsync(int gameId, CancellationToken ct = default);
    }
}