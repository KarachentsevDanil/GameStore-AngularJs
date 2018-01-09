using System.Collections.Generic;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Games;
using Microsoft.AspNetCore.Mvc;

namespace GSP.WebClient.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    public class RateController : Controller
    {
        private readonly IRateService _rateService;
        private readonly IGameService _gameService;
        private readonly IOrderService _orderService;

        public RateController(IRateService rateService, IGameService gameService, IOrderService orderService)
        {
            _rateService = rateService;
            _gameService = gameService;
            _orderService = orderService;
        }

        [HttpGet]
        public IEnumerable<Rate> GetGameRates(int gameId)
        {
            return _rateService.GetRatesOfGame(gameId);
        }
        
        [HttpPost]
        public void CreateFeed([FromBody] Rate rate)
        {
            _rateService.AddFeedbackToGame(rate);
        }

        //[HttpGet]
        //public async Task<GameResponse[]> GetRecomendedGames(int gameId)
        //{
        //    var transactions = await orderService.GetAllTransaction();
        //    GetRecomendation recomendation = new GetRecomendation();
        //    var recomendItems = recomendation.Recomendation(transactions, gameId);

        //    var gameRecomendationList = new List<GameResponse>();

        //    foreach (var item in recomendItems)
        //    {
        //        gameRecomendationList.Add(await gameService.GetGameByIdAsync(item));
        //    }

        //    return gameRecomendationList.ToArray();
        //}
    }
}
