using GSP.Common.BLL.Services.Contracts;
using GSP.Common.Domain.Params;
using GSP.Common.Web.Models;
using GSP.Games.BLL.Dto.Game;
using GSP.Games.BLL.Services.Contracts;
using GSP.Games.Domain.Params;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSP.Games.WebService.ApiControllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
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
        public IActionResult GetGameById(int id)
        {
            var game = _gameService.GetGameById(id);
            return Json(JsonResultData.Success(game));
        }

        [HttpPost]
        public IActionResult GetGamesByParams([FromBody] GamesFilterParams filterParams)
        {
            var games = _gameService.GetGamesByParams(filterParams, out var totalCount);
            
            var result = new CollectionResult<GameDto>
            {
                Collection = games,
                TotalCount = totalCount
            };

            return Json(JsonResultData.Success(result));
        }
    }
}
