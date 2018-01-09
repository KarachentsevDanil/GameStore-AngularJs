using System.Collections.Generic;
using GSP.Domain.Games;
using GSP.Domain.Params;

namespace GSP.BLL.Services.Contracts
{
    public interface IRateService
    {
        void AddFeedbackToGame(Rate rate);

        IEnumerable<Rate> GetRatesOfGame(int gameId);

        IEnumerable<Game> GetTopRateGames(FilterParams<Game> filterParams);

        IEnumerable<Game> GetTopSellGames(FilterParams<Game> filterParams);
    }
}
