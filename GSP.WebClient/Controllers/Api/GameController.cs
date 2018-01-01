using System.Collections.Generic;
using System.Linq;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Games;
using GSP.WebClient.Infrastracture.Extenctions;
using GSP.WebClient.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GSP.WebClient.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly ICategoryService _categoryService;

        public GameController(IGameService gameService, ICategoryService categoryService)
        {
            _gameService = gameService;
            _categoryService = categoryService;
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
            _gameService.UpdateGame(game);
        }

        [HttpGet]
        public IEnumerable<GameViewModel> GetGames()
        {
            var games = _gameService.GetGames().ToList();
            return ToGameViewModels(games);
        }

        [HttpGet]
        public Game GetGameById(int gameId)
        {
            return _gameService.GetGameById(gameId);
        }

        [HttpGet]
        public IEnumerable<GameViewModel> GetGamesByName(string name)
        {
            var games = _gameService.GetGamesByTerm(name);
            return ToGameViewModels(games);
        }

        [HttpGet]
        public IEnumerable<GameViewModel> GetGamesByCategory(string category)
        {
            var categoryId = _categoryService.GetCategoryByName(category).CategoryId;
            var games = _gameService.GetGamesByCategory(categoryId);
            return ToGameViewModels(games);
        }

        private IEnumerable<GameViewModel> ToGameViewModels(IEnumerable<Game> games)
        {
            var result = new List<GameViewModel>();

            foreach (var game in games)
            {
                result.Add(MapperExtenctions.ToGameViewModel(game));
            }

            return result;
        }
    }
}
