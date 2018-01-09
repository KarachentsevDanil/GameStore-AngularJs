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
        public Result<GameViewModel> GetGamesByParams([FromBody] GamesFilterParams filterParams)
        {
            int totalCount;

            SetGamesFilterParams(filterParams);
            var games = GetGamesByFilterParams(filterParams, filterParams.OutputMode, out totalCount);
            var gamesViewModel = MapperExtenctions.ToGameViewModels(games);

            var result = new Result<GameViewModel>
            {
                Collection = gamesViewModel,
                TotalCount = totalCount
            };

            return result;
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

        private void SetGamesFilterParams(GamesFilterParams filterParams)
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

            filterParams.Expression = predicate;
        }

        private IEnumerable<Game> GetGamesByFilterParams(FilterParams<Game> filterParams, GamesOutputMode mode, out int totalCount)
        {
            totalCount = 10;
            switch (mode)
            {
                case GamesOutputMode.All:
                    return _gameService.GetGamesByParams(filterParams, out totalCount);
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
