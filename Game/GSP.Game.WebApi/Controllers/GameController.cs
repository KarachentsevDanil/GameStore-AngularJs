using GSP.BLL.Services.Contracts;
using GSP.Game.BLL.DTOs.Game;
using GSP.Game.BLL.Services.Contracts;
using GSP.Game.Domain.Params;
using GSP.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GSP.Game.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        private readonly ICacheService _cacheService;

        public GameController(IGameService gameService, ICacheService cacheService)
        {
            _gameService = gameService;
            _cacheService = cacheService;
        }

        [HttpPost]
        public async Task<JsonResultData> CreateGame([FromBody] CreateGameDto game)
        {
            await _gameService.AddGameAsync(game);
            return JsonResultData.Success();
        }

        [HttpDelete("{id}")]
        public async Task<JsonResultData> DeleteGame([FromBody] int id)
        {
            await _gameService.DeleteGameAsync(id);
            return JsonResultData.Success();
        }

        [HttpPut("{id}")]
        public async Task<JsonResultData> EditGame(int id, [FromBody] GameDto game)
        {
            await _gameService.UpdateGameAsync(game);
            return JsonResultData.Success();
        }
        
        [HttpGet("{id}")]
        public async Task<JsonResultData> GetGameById(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            return JsonResultData.Success(game);
        }

        [HttpPost]
        [Route("getgamesbyparams")]
        public async Task<JsonResultData> GetGamesByParams([FromBody] GamesFilterParams filterParams)
        {
            var games = await _gameService.GetGamesByParamsAsync(filterParams);
            return JsonResultData.Success(games);
        }
    }
}