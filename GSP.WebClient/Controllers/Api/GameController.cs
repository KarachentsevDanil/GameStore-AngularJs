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
        private readonly ICategoryService _categoryService;
        private readonly ICustomerService _customerService;
        private readonly IRateService _rateService;

        public GameController(
            IGameService gameService,
            ICategoryService categoryService,
            ICustomerService customerService,
            IRateService rateService)
        {
            _gameService = gameService;
            _categoryService = categoryService;
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
        public IEnumerable<GameViewModel> GetGamesByParams([FromBody] GamesFilterParams filterParams)
        {
            var gamesFilterParams = GetGamesFilterParams(filterParams);
            var games = GetGamesByFilterParams(gamesFilterParams, filterParams.OutputMode);
            return MapperExtenctions.ToGameViewModels(games);
        }

        [HttpGet]
        public IEnumerable<GameViewModel> GetGamesByName(string name)
        {
            var games = _gameService.GetGamesByTerm(name ?? string.Empty);
            return MapperExtenctions.ToGameViewModels(games);
        }

        [HttpGet]
        public IEnumerable<GameViewModel> GetGamesByCategory(string category)
        {
            var categoryId = _categoryService.GetCategoryByName(category).CategoryId;
            var games = _gameService.GetGamesByCategory(categoryId);
            return MapperExtenctions.ToGameViewModels(games);
        }

        private FilterParams<Game> GetGamesFilterParams(GamesFilterParams filterParams)
        {
            var predicate = PredicateBuilder.New<Game>(x => !x.IsDeleted);

            if (filterParams.CategoriesIds != null && filterParams.CategoriesIds.Any())
            {
                predicate = predicate.Extend(p => filterParams.CategoriesIds.Contains(p.CategoryId), PredicateOperator.And);
            }
            ;
            if (!string.IsNullOrEmpty(filterParams.Term))
            {
                predicate = predicate.Extend(x => x.Name.ToLower().Contains(filterParams.Term.ToLower()), PredicateOperator.And);
            }

            if (filterParams.StartPrice.HasValue && filterParams.EndPrice.HasValue)
            {
                predicate = predicate.Extend(x => filterParams.StartPrice <= x.Price && filterParams.EndPrice >= x.Price, PredicateOperator.And);
            }

            if (!string.IsNullOrEmpty(filterParams.Customer))
            {
                var customer = _customerService.GetCustomerByTerm(filterParams.Customer);
                predicate = predicate.Extend(x => x.Orders.Any(o => o.Order.CustomerId == customer.CustomerId));
            }

            if (!string.IsNullOrEmpty(filterParams.Customer))
            {
                var customer = _customerService.GetCustomerByTerm(filterParams.Customer);
                predicate = predicate.Extend(x => x.Orders.Any(o => o.Order.CustomerId == customer.CustomerId));
            }

            if (filterParams.OutputMode == GamesOutputMode.TopRate)
            {
                predicate = predicate.Extend(x => x.Rates.Any());
            }

            if (filterParams.OutputMode == GamesOutputMode.TopSell)
            {
                predicate = predicate.Extend(x => x.Orders.Any());
            }

            var gameParams = new FilterParams<Game>
            {
                Expression = predicate
            };

            return gameParams;
        }

        private IEnumerable<Game> GetGamesByFilterParams(FilterParams<Game> filterParams, GamesOutputMode mode)
        {
            switch (mode)
            {
                case GamesOutputMode.All:
                    return _gameService.GetGamesByParams(filterParams);
                case GamesOutputMode.TopSell:
                    return _rateService.GetTopSellGames(filterParams);
                case GamesOutputMode.TopRate:
                    return _rateService.GetTopRateGames(filterParams);
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, "Wrong option was sent.");
            }
        }
    }
}
