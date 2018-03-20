using System.Collections.Generic;
using GSP.BLL.Dto.Game;
using GSP.BLL.Services.Cache;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Params;
using GSP.SPA.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GSP.SPA.Controllers.Api
{
    [Route("api/[controller]/[action]/{id?}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly ICacheService _cacheService;

        public GameController(IGameService gameService, ICacheService cacheService)
        {
            _gameService = gameService;
            _cacheService = cacheService;
        }

        [HttpPost]
        public IActionResult CreateGame([FromBody] CreateGameDto game)
        {
            _gameService.AddGame(game);
            return Json(JsonResultData.Success());
        }

        [HttpPost]
        public IActionResult DeleteGame([FromBody] int id)
        {
            _gameService.DeleteGame(id);
            return Json(JsonResultData.Success());
        }

        [HttpPost]
        public IActionResult EditGame([FromBody] GameDto game)
        {
            _gameService.UpdateGame(game);
            return Json(JsonResultData.Success());
        }

        [HttpGet]
        public IActionResult GetRecomendedGames(int id)
        {
            var games = _gameService.GetRecomendedGames(id);

            FillCustomerGamesFlag(games);

            return Json(JsonResultData.Success(games));
        }

        [HttpGet]
        public IActionResult GetGameById(int id)
        {
            var game = _gameService.GetGameById(id);

            var customerGames = GetCustomerGames();

            if (customerGames.Any())
            {
                game.IsGameBought = customerGames.Any(g => g.GameId == game.GameId);
            }

            return Json(JsonResultData.Success(game));
        }

        [HttpPost]
        public IActionResult GetGamesByParams([FromBody] GamesFilterParams filterParams)
        {
            var games = _gameService.GetGamesByParams(filterParams, out var totalCount);

            FillCustomerGamesFlag(games);

            var result = new CollectionResult<GameDto>
            {
                Collection = games,
                TotalCount = totalCount
            };

            return Json(JsonResultData.Success(result));
        }

        private void FillCustomerGamesFlag(IEnumerable<GameDto> games)
        {
            var customerGames = GetCustomerGames();

            if (customerGames.Any())
            {
                foreach (var game in games)
                {
                    game.IsGameBought = customerGames.Any(g => g.GameId == game.GameId);
                }
            }
        }

        private List<GameDto> GetCustomerGames()
        {
            var games = _cacheService.Get<List<GameDto>>($"{CacheKey.CustomerGames}_{User.Identity.Name}", CacheBucket.CustomerGames);

            if (games == null)
            {
                games = _gameService.GetCustomerGames(User.Identity.Name).ToList();
                _cacheService.Add(games, $"{CacheKey.CustomerGames}_{User.Identity.Name}", CacheBucket.CustomerGames);
            }

            return games ?? new List<GameDto>();
        }
    }
}
