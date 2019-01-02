using GSP.BLL.Constants;
using GSP.BLL.Enums;
using GSP.BLL.Services.Contracts;
using GSP.Order.BLL.DTOs.Order;
using GSP.Order.BLL.Services.Contracts;
using GSP.Order.Domain.Params;
using GSP.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSP.Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        private readonly ICacheService _cacheService;

        public OrderController(
            IOrderService orderService,
            ICacheService cacheService)
        {
            _orderService = orderService;
            _cacheService = cacheService;
        }

        [HttpPost]
        public async Task<JsonResultData> ConfirmOrder([FromBody] CompleteOrderDto order)
        {
            await _orderService.UpdateOrderAsync(order);

            _cacheService.ResetBucket($"{CacheKey.Bucket}_{order.CustomerId}");
            _cacheService.ResetCustomerGames($"{CacheKey.CustomerGames}_{order.CustomerId}");

            return JsonResultData.Success();
        }

        [HttpPost]
        public async Task<JsonResultData> AddToBucket([FromBody] AddGameToBucketDto orderGame)
        {
            var order = await GetCurrentOrderOfCustomer(orderGame.CustomerId);

            if (order.Games.Any(x => x.GameId == orderGame.GameId))
            {
                return JsonResultData.Error("Game was already added to bucket");
            }

            var customerGames = await _orderService.GetCustomerGamesAsync(order.CustomerId);

            if (customerGames.Any(x => x.GameId == orderGame.GameId))
            {
                return JsonResultData.Error("Game was already bought");
            }

            orderGame.OrderId = order.OrderId;

            await _orderService.AddGameToBucketAsync(orderGame);

            _cacheService.ResetBucket($"{CacheKey.Bucket}_{orderGame.CustomerId}");

            return JsonResultData.Success();
        }

        [HttpPost]
        public async Task<JsonResultData> DeleteGameFromBucket([FromBody] AddGameToBucketDto orderGame)
        {
            await _orderService.DeleteGameFromBucketAsync(orderGame);

            _cacheService.ResetBucket($"{CacheKey.Bucket}_{orderGame.CustomerId}");

            return JsonResultData.Success();
        }

        [HttpPost]
        public async Task<JsonResultData> DeleteOrder([FromBody] int orderId)
        {
            await _orderService.DeleteOrderAsync(orderId);

            return JsonResultData.Success();
        }

        [HttpPost]
        public async Task<JsonResultData> GetGamesFromBucket([FromBody] OrderDto order)
        {
            var games = _cacheService.Get<IEnumerable<OrderGameDto>>($"{CacheKey.Bucket}_{order.CustomerId}", CacheBucket.Bucket);

            if (games == null)
            {
                games = await _orderService.GetGameFromBucketAsync(order.CustomerId);
                _cacheService.Add(games, $"{CacheKey.Bucket}_{order.CustomerId}", CacheBucket.Bucket);
            }

            return JsonResultData.Success(games);
        }

        [HttpPost]
        public async Task<JsonResultData> GetOrdersByParams([FromBody]OrdersFilterParams filterParams)
        {
            var orders = await _orderService.GetOrdersByParamsAsync(filterParams);
            
            return JsonResultData.Success(orders);
        }

        private async Task<OrderDto> GetCurrentOrderOfCustomer(string customerId)
        {
            var order = await _orderService.GetCurrentOrderOfCustomerAsync(customerId);

            if (order == null)
            {
                var newOrder = new CreateOrderDto
                {
                    CustomerId = customerId
                };

                await _orderService.AddOrderAsync(newOrder);
                order = await _orderService.GetCurrentOrderOfCustomerAsync(customerId);
            }

            return order;
        }
    }
}