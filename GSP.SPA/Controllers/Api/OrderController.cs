using System;
using System.Collections.Generic;
using System.Linq;
using GSP.BLL.Dto.Game;
using GSP.BLL.Dto.Order;
using GSP.BLL.Resources;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Params;
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

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public void ConfirmOrder([FromBody] OrderDto order)
        {
            _orderService.UpdateOrder(order);
        }

        [HttpPost]
        public void AddToBucket([FromBody] AddGameToBucketDto orderGame)
        {
            var order = GetCurrentOrderOfCustomer(orderGame.CustomerId);

            if (order.Games.Any(x => x.GameId == orderGame.GameId))
            {
                throw new Exception(Exceptions.GameAlreadyInBucket);
            }

            var customerGames = _orderService.GetCustomerGames(order.CustomerId);
            if (customerGames.Any(x => x.GameId == orderGame.GameId))
            {
                throw new Exception(Exceptions.GameAlreadyBought);
            }

            orderGame.OrderId = order.OrderId;

            _orderService.AddGameToBucket(orderGame);
        }

        [HttpPost]
        public void DeleteGameFromBucket([FromBody] AddGameToBucketDto orderGame)
        {
            _orderService.DeleteGameFromBucket(orderGame);
        }

        [HttpPost]
        public void DeleteOrder([FromBody] int orderId)
        {
            _orderService.DeleteOrder(orderId);
        }

        [HttpPost]
        public IEnumerable<GameDto> GetGamesFromBucket([FromBody] OrderDto order)
        {
            var games = _orderService.GetGameFromBucket(order.CustomerId);
            return games;
        }

        [HttpPost]
        public CollectionResult<OrderDto> GetOrdersByParams([FromBody]OrdersFilterParams filterParams)
        {
            var orders = _orderService.GetOrdersByParams(filterParams, out var totalCount);

            var result = new CollectionResult<OrderDto>
            {
                Collection = orders,
                TotalCount = totalCount
            };

            return result;
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
