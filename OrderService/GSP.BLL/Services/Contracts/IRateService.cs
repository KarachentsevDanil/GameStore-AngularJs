using System.Collections.Generic;
using GSP.BLL.Dto.Rate;

namespace GSP.BLL.Services.Contracts
{
    public interface IRateService
    {
        void AddFeedbackToGame(CreateRateDto rate);

        IEnumerable<RateDto> GetRatesOfGame(int gameId);
    }
}
