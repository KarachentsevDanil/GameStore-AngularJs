using System;
using System.Collections.Generic;
using System.Linq;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Games;
using GSP.Domain.Orders;
using GSP.WebClient.Infrastracture.Extenctions;
using GSP.WebClient.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GSP.WebClient.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController(IOrderService orderService, ICustomerService customerService)
        {
            _orderService = orderService;
            _customerService = customerService;
        }

        [HttpPost]
        public void CreateOrder([FromBody] OrderViewModel order)
        {
            var customerId = _customerService.GetCustomerByTerm(order.Customer).CustomerId;
            var newOrder = new Order { CustomerId = customerId };
            _orderService.AddOrder(newOrder);
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
        public void DeleteGameFromOrder([FromBody] OrderGame order)
        {

        }

        [HttpPost]
        public void DeleteOrder([FromBody] int orderId)
        {
            _orderService.DeleteOrder(orderId);
        }

        [HttpGet]
        public IEnumerable<GameViewModel> GetGamesFromBucket(string customer)
        {
            var customerId = _customerService.GetCustomerByTerm(customer).CustomerId;
            var games = _orderService.GetGameFromBucket(customerId);
            return MapperExtenctions.ToGameViewModels(games);
        }

        [HttpGet]
        public IEnumerable<GameViewModel> GetCusomerGame(string customer)
        {
            var customerId = _customerService.GetCustomerByTerm(customer).CustomerId;
            var games = _orderService.GetCustomerGames(customerId);
            return MapperExtenctions.ToGameViewModels(games);
        }

        [HttpGet]
        public IEnumerable<OrderViewModel> GetCustomerOrder(string customer)
        {
            var customerId = _customerService.GetCustomerByTerm(customer).CustomerId;
            var orders = _orderService.GetCustomerOrders(customerId);
            return MapperExtenctions.ToOrderViewModels(orders);
        }

        [HttpGet]
        public IEnumerable<OrderViewModel> GetAllOrder()
        {
            var orders = _orderService.GetOrders();
            return MapperExtenctions.ToOrderViewModels(orders);
        }

        private Order GetCurrentOrderOfCustomer(string customerName)
        {
            var customer = _customerService.GetCustomerByTerm(customerName);
            var order = _orderService.GetCurrentOrderOfCustomer(customer.CustomerId);
            return order;
        }
    }
}
