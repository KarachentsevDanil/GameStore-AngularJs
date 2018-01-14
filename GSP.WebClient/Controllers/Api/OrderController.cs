using System;
using System.Collections.Generic;
using System.Linq;
using GSP.BLL.Resources;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Orders;
using GSP.Domain.Params;
using GSP.WebClient.Infrastracture.Extenctions;
using GSP.WebClient.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GSP.WebClient.Controllers.Api
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
        public void CreateOrder([FromBody] OrderViewModel order)
        {
            var currentOrder = GetCurrentOrderOfCustomer(order.Customer);

            if (currentOrder == null)
            {
                var customer = GetCustomerByTerm(order.Customer);
                var newOrder = new Order { CustomerId = customer.CustomerId };
                _orderService.AddOrder(newOrder);
            }
        }

        [HttpPost]
        public void ConfirmOrder([FromBody] OrderViewModel order)
        {
            var orderModel = GetCurrentOrderOfCustomer(order.Customer);

            orderModel.SaleDate = DateTime.Now;
            orderModel.Status = OrderStatus.Complete;

            _orderService.UpdateOrder(orderModel);
        }

        [HttpPost]
        public void AddToBucket([FromBody] OrderGameViewModel orderGame)
        {
            var order = GetCurrentOrderOfCustomer(orderGame.Customer);
            if (order.Games.Any(x => x.GameId == orderGame.GameId))
            {
                throw new Exception(Exceptions.GameAlreadyInBucket);
            }

            var customerGames = _orderService.GetCustomerGames(order.CustomerId);
            if (customerGames.Any(x => x.GameId == orderGame.GameId))
            {
                throw new Exception(Exceptions.GameAlreadyBought);
            }

            var newOrderGame = new OrderGame(orderGame.GameId, order.OrderId);

            _orderService.AddGameToBucket(newOrderGame);
        }

        [HttpPost]
        public void DeleteGameFromBucket([FromBody] OrderGameViewModel orderGame)
        {
            var order = GetCurrentOrderOfCustomer(orderGame.Customer);
            var game = order.Games.First(x => x.GameId == orderGame.GameId);

            _orderService.DeleteGameFromBucket(game);
        }

        [HttpPost]
        public void DeleteOrder([FromBody] int orderId)
        {
            _orderService.DeleteOrder(orderId);
        }

        [HttpGet]
        public IEnumerable<GameViewModel> GetGamesFromBucket(string customer)
        {
            var customerModel = _customerService.GetCustomerByTerm(customer);
            var games = _orderService.GetGameFromBucket(customerModel.CustomerId);
            return MapperExtenctions.ToGameViewModels(games);
        }

        [HttpPost]
        public CollectionResult<OrderViewModel> GetOrdersByParams([FromBody]OrdersFilterParams filterParams)
        {
            SetAdditionalParams(filterParams);

            var orders = _orderService.GetOrdersByParams(filterParams, out var totalCount);
            var ordersViewModel = MapperExtenctions.ToOrderViewModels(orders);

            var result = new CollectionResult<OrderViewModel>
            {
                Collection = ordersViewModel,
                TotalCount = totalCount
            };

            return result;
        }

        private Order GetCurrentOrderOfCustomer(string customerName)
        {
            var customer = GetCustomerByTerm(customerName);
            var order = _orderService.GetCurrentOrderOfCustomer(customer.CustomerId);
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
