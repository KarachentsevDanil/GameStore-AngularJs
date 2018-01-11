using System;
using System.Collections.Generic;
using System.Linq;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Games;
using GSP.Domain.Params;
using GSP.WebClient.Infrastracture.Extenctions;
using GSP.WebClient.ViewModels;
using LinqKit;
using Microsoft.AspNetCore.Mvc;

namespace GSP.WebClient.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly ICustomerService _customerService;
        private readonly IRateService _rateService;

        public GameController(
            IGameService gameService,
            ICustomerService customerService,
            IRateService rateService)
        {
            _gameService = gameService;
            _customerService = customerService;
            _rateService = rateService;
        }

        [HttpPost]
        public void CreateGame([FromBody] Game game)
        {
            _gameService.AddGame(game);
        }

        [HttpPost]
        public void DeleteGame([FromBody] int gameId)
        {
            _gameService.DeleteGame(gameId);
        }

        [HttpPost]
        public void EditGame([FromBody] Game game)
        {
            var oldGame = _gameService.GetGameById(game.GameId);

            oldGame.Name = game.Name;
            oldGame.Description = game.Description;
            oldGame.Price = game.Price;

            _gameService.UpdateGame(oldGame);
        }

        [HttpGet]
        public IEnumerable<GameViewModel> GetGames()
        {
            var games = _gameService.GetGames().ToList();
            return MapperExtenctions.ToGameViewModels(games);
        }

        [HttpGet]
        public GameViewModel GetGameById(int gameId)
        {
            var game = _gameService.GetGameById(gameId);
            return MapperExtenctions.ToGameViewModel(game);
        }

        [HttpPost]
        public Result<GameViewModel> GetGamesByParams([FromBody] GamesFilterParams filterParams)
        {
            SetAdditionalParams(filterParams);
            var games = _gameService.GetGamesByParams(filterParams, out var totalCount);
            var gamesViewModel = MapperExtenctions.ToGameViewModels(games);

            var result = new Result<GameViewModel>
            {
                Collection = gamesViewModel,
                TotalCount = totalCount
            };

            return result;
        }

        private void SetAdditionalParams(GamesFilterParams @params)
        {
            if (!string.IsNullOrEmpty(@params.Customer))
            {
                var customer = _customerService.GetCustomerByTerm(@params.Customer);
                @params.CustomerId = customer.CustomerId;
            }
        }
    }
}
