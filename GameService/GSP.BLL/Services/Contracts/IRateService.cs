using System.Collections.Generic;
using GSP.Games.BLL.Dto.Rate;

namespace GSP.Games.BLL.Services.Contracts
{
    public interface IRateService
    {
        void AddFeedbackToGame(CreateRateDto rate);

        IEnumerable<RateDto> GetRatesOfGame(int gameId);
    }
}
