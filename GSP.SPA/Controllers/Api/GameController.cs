using System.Collections.Generic;
using GSP.BLL.Dto.Game;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Params;
using Microsoft.AspNetCore.Mvc;

namespace GSP.SPA.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    public class GameController : BaseGameStoreController
    {
        private readonly IGameService _gameService;

        public GameController(
            IGameService gameService,
            ICustomerService customerService,
            IRateService rateService) : base(customerService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public void CreateGame([FromBody] CreateGameDto game)
        {
            _gameService.AddGame(game);
        }

        [HttpPost]
        public void DeleteGame([FromBody] int gameId)
        {
            _gameService.DeleteGame(gameId);
        }

        [HttpPost]
        public void EditGame([FromBody] CreateGameDto game)
        {
            _gameService.UpdateGame(game);
        }

        [HttpGet]
        public IEnumerable<GameDto> GetRecomendedGames(int gameId)
        {
            var games = _gameService.GetRecomendedGames(gameId);
            return games;
        }

        [HttpGet]
        public GameDto GetGameById(int gameId)
        {
            var game = _gameService.GetGameById(gameId);
            return game;
        }

        [HttpPost]
        public CollectionResult<GameDto> GetGamesByParams([FromBody] GamesFilterParams filterParams)
        {
            SetAdditionalParams(filterParams);

            var games = _gameService.GetGamesByParams(filterParams, out var totalCount);

            var result = new CollectionResult<GameDto>
            {
                Collection = games,
                TotalCount = totalCount
            };

            return result;
        }

        private void SetAdditionalParams(GamesFilterParams @params)
        {
            if (!string.IsNullOrEmpty(@params.Customer))
            {
                var customer = GetCustomerByTerm(@params.Customer);
                @params.CustomerId = customer.CustomerId;
            }
        }
    }
}
