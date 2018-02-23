using System.Collections.Generic;
using GSP.BLL.Dto.Game;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Params;
using Microsoft.AspNetCore.Mvc;

namespace GSP.SPA.Controllers.Api
{
    [Route("api/[controller]/[action]/{id?}")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public void CreateGame([FromBody] CreateGameDto game)
        {
            _gameService.AddGame(game);
        }

        [HttpPost]
        public void DeleteGame([FromBody] int id)
        {
            _gameService.DeleteGame(id);
        }

        [HttpPost]
        public void EditGame([FromBody] GameDto game)
        {
            _gameService.UpdateGame(game);
        }

        [HttpGet]
        public IEnumerable<GameDto> GetRecomendedGames(int id)
        {
            var games = _gameService.GetRecomendedGames(id);
            return games;
        }

        [HttpGet]
        public GameDto GetGameById(int id)
        {
            var game = _gameService.GetGameById(id);
            return game;
        }

        [HttpPost]
        public CollectionResult<GameDto> GetGamesByParams([FromBody] GamesFilterParams filterParams)
        {
            var games = _gameService.GetGamesByParams(filterParams, out var totalCount);

            var result = new CollectionResult<GameDto>
            {
                Collection = games,
                TotalCount = totalCount
            };

            return result;
        }
    }
}
