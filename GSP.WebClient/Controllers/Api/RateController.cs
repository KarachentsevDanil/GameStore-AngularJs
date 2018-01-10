using System;
using System.Collections.Generic;
using AutoMapper;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Games;
using GSP.WebClient.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GSP.WebClient.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    public class RateController : Controller
    {
        private readonly IRateService _rateService;
        private readonly IGameService _gameService;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        public RateController(IRateService rateService, IGameService gameService, IOrderService orderService, ICustomerService customerService)
        {
            _rateService = rateService;
            _gameService = gameService;
            _orderService = orderService;
            _customerService = customerService;
        }

        [HttpGet]
        public IEnumerable<RateViewModel> GetGameRates(int gameId)
        {
            var rates =_rateService.GetRatesOfGame(gameId);
            var ratesViewModel = Mapper.Map<IEnumerable<Rate>, IEnumerable<RateViewModel>>(rates);
            return ratesViewModel;
        }
        
        [HttpPost]
        public void CreateFeed([FromBody] RateViewModel rateViewModel)
        {
            var customer = _customerService.GetCustomerByTerm(rateViewModel.Customer);

            var rate = new Rate
            {
                Comment = rateViewModel.Comment,
                GameId = rateViewModel.GameId,
                CustomerId = customer.CustomerId,
                Rating = rateViewModel.Rating,
                CreatedOn = DateTime.Now
            };

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
