using System.Collections.Generic;
using GSP.Domain.Games;

namespace GSP.BLL.Services.Contracts
{
    public interface IRateService
    {
        void AddFeedbackToGame(Rate rate);

        IEnumerable<Rate> GetRatesOfGame(int gameId);
    }
}
