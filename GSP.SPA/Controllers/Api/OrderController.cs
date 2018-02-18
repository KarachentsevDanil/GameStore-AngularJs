using System;
using System.Collections.Generic;
using System.Linq;
using GSP.BLL.Dto.Game;
using GSP.BLL.Dto.Order;
using GSP.BLL.Resources;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Orders;
using GSP.Domain.Params;
using Microsoft.AspNetCore.Mvc;

namespace GSP.SPA.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    public class OrderController : BaseGameStoreController
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController(IOrderService orderService, ICustomerService customerService) : base(customerService)
        {
            _orderService = orderService;
            _customerService = customerService;
        }

        [HttpPost]
        public void CreateOrder([FromBody] CreateOrderDto order)
        {
            var currentOrder = GetCurrentOrderOfCustomer(order.CustomerId);

            if (currentOrder == null)
            {
                _orderService.AddOrder(order);
            }
        }

        [HttpPost]
        public void ConfirmOrder([FromBody] CompleteOrderDto order)
        {
            order.SaleDate = DateTime.Now;
            order.Status = OrderStatus.Complete;

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

        [HttpGet]
        public IEnumerable<GameDto> GetGamesFromBucket(string customerId)
        {
            var games = _orderService.GetGameFromBucket(customerId);
            return games;
        }

        [HttpPost]
        public CollectionResult<OrderDto> GetOrdersByParams([FromBody]OrdersFilterParams filterParams)
        {
            SetAdditionalParams(filterParams);

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
            return order;
        }

        private void SetAdditionalParams(OrdersFilterParams @params)
        {
            if (!string.IsNullOrEmpty(@params.Customer))
            {
                var customer = GetCustomerByTerm(@params.Customer);
                @params.CustomerId = customer.CustomerId;
            }
        }
    }
}
