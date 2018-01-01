using System;
using System.Collections.Generic;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Games;
using GSP.Domain.Orders;
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
        public void ConfirmOrder([FromBody] Order order)
        {
            order.SaleDate = DateTime.Now;
            _orderService.UpdateOrder(order);
        }

        [HttpPost]
        public void AddToBucket([FromBody] OrderGame orderGame)
        {
            _orderService.AddGameToBucket(orderGame);
        }

        [HttpPost]
        public void DeleteGameFromBucket([FromBody] OrderGame orderGame)
        {
            _orderService.DeleteGameFromBucket(orderGame);
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
        public IEnumerable<Game> GetGamesFromBucket(string customer)
        {
            var customerId = _customerService.GetCustomerByTerm(customer).CustomerId;
            return _orderService.GetGameFromBucket(customerId);
        }

        [HttpGet]
        public IEnumerable<Game> GetCusomerGame(string customer)
        {
            var customerId = _customerService.GetCustomerByTerm(customer).CustomerId;
            return _orderService.GetCustomerGames(customerId);
        }

        [HttpGet]
        public IEnumerable<Order> GetCustomerOrder(string customer)
        {
            var customerId = _customerService.GetCustomerByTerm(customer).CustomerId;
            return _orderService.GetCustomerOrders(customerId);
        }

        [HttpGet]
        public IEnumerable<Order> GetAllOrder()
        {
            return _orderService.GetOrders();
        }
    }
}
