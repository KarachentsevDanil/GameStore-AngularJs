using System;
using System.IO;
using System.Linq;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Games;
using GSP.WebClient.Infrastracture.Extenctions;
using GSP.WebClient.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace GSP.WebClient.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public IActionResult AddGame()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddGame(IFormFile file, Game game)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var fileStream = file.OpenReadStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        game.Photo = memoryStream.ToArray();
                    }
                }
            }

            _gameService.AddGame(game);

            return RedirectToAction("EditOrDeleteGame");
        }

        [HttpGet]
        public IActionResult GameProfile(int gameId)
        {
            var game = new GameViewModel { GameId = gameId, Ratings = EnumExnections.ConvertEnumValuesToDictionary<Rating>()};
            return View(game);
        }

        [HttpGet]
        public IActionResult OwnGameList()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditOrDeleteGame()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ShowGame()
        {
            return View();
        }
    }
}