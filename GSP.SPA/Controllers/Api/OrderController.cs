using System;
using System.Collections.Generic;
using System.Linq;
using GSP.BLL.Dto.Game;
using GSP.BLL.Dto.Order;
using GSP.BLL.Resources;
using GSP.BLL.Services.Cache;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Params;
using GSP.SPA.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSP.SPA.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICacheService _cacheService;
        
        public OrderController(IOrderService orderService, ICacheService cacheService)
        {
            _orderService = orderService;
            _cacheService = cacheService;
        }

        [HttpPost]
        public IActionResult ConfirmOrder([FromBody] CompleteOrderDto order)
        {
            _orderService.UpdateOrder(order);

            _cacheService.ResetBucket($"{CacheKey.Bucket}_{order.CustomerId}");
            _cacheService.ResetBucket($"{CacheKey.CustomerGames}_{User.Identity.Name}");

            return Json(JsonResultData.Success());
        }

        [HttpPost]
        public IActionResult AddToBucket([FromBody] AddGameToBucketDto orderGame)
        {
            var order = GetCurrentOrderOfCustomer(orderGame.CustomerId);

            if (order.Games.Any(x => x.GameId == orderGame.GameId))
            {
                return Json(JsonResultData.Error(Exceptions.GameAlreadyInBucket));
            }

            var customerGames = _orderService.GetCustomerGames(order.CustomerId);
            if (customerGames.Any(x => x.GameId == orderGame.GameId))
            {
                return Json(JsonResultData.Error(Exceptions.GameAlreadyBought));
            }

            orderGame.OrderId = order.OrderId;
            _orderService.AddGameToBucket(orderGame);
            _cacheService.ResetBucket($"{CacheKey.Bucket}_{orderGame.CustomerId}");

            return Json(JsonResultData.Success());
        }

        [HttpPost]
        public IActionResult DeleteGameFromBucket([FromBody] AddGameToBucketDto orderGame)
        {
            _orderService.DeleteGameFromBucket(orderGame);
            _cacheService.ResetBucket($"{CacheKey.Bucket}_{orderGame.CustomerId}");

            return Json(JsonResultData.Success());
        }

        [HttpPost]
        public IActionResult DeleteOrder([FromBody] int orderId)
        {
            _orderService.DeleteOrder(orderId);

            return Json(JsonResultData.Success());
        }

        [HttpPost]
        public IActionResult GetGamesFromBucket([FromBody] OrderDto order)
        {
            var games = _cacheService.Get<IEnumerable<GameDto>>($"{CacheKey.Bucket}_{order.CustomerId}", CacheBucket.Bucket);

            if (games == null)
            {
                games = _orderService.GetGameFromBucket(order.CustomerId);
                _cacheService.Add(games, $"{CacheKey.Bucket}_{order.CustomerId}", CacheBucket.Bucket);
            }
            
            return Json(JsonResultData.Success(games));
        }

        [HttpPost]
        public IActionResult GetOrdersByParams([FromBody]OrdersFilterParams filterParams)
        {
            var orders = _orderService.GetOrdersByParams(filterParams, out var totalCount);

            var result = new CollectionResult<OrderDto>
            {
                Collection = orders,
                TotalCount = totalCount
            };
            
            return Json(JsonResultData.Success(result));
        }

        private OrderDto GetCurrentOrderOfCustomer(string customerId)
        {
            var order = _orderService.GetCurrentOrderOfCustomer(customerId);

            if (order == null)
            {
                var newOrder = new CreateOrderDto
                {
                    CustomerId = customerId
                };

                _orderService.AddOrder(newOrder);
                order = _orderService.GetCurrentOrderOfCustomer(customerId);
            }

            return order;
        }
    }
}
