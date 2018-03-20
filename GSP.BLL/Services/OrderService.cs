using System;
using System.Collections.Generic;
using System.Linq;
using GSP.BLL.Dto.Game;
using GSP.BLL.Dto.Order;
using GSP.BLL.Services.Contracts;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Domain.Games;
using GSP.Domain.Orders;
using GSP.Domain.Params;

namespace GSP.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGameStoreUnitOfWork _unitOfWork;

        public OrderService(IGameStoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int AddOrder(CreateOrderDto order)
        {
            var newOrder = AutoMapper.Mapper.Map<CreateOrderDto, Order>(order);

            _unitOfWork.OrderRepository.Add(newOrder);
            _unitOfWork.Commit();

            return newOrder.OrderId;
        }

        public void UpdateOrder(CompleteOrderDto order)
        {
            var completedOrder = _unitOfWork.OrderRepository.GetCurrentCustomerOrder(order.CustomerId);

            completedOrder.SaleDate = DateTime.Now;
            completedOrder.Status = OrderStatus.Complete;
            completedOrder.PaymentId = order.PaymentId;

            _unitOfWork.OrderRepository.Update(completedOrder);
            _unitOfWork.Commit();
        }

        public void DeleteOrder(int orderId)
        {
            _unitOfWork.OrderRepository.Delete(orderId);
            _unitOfWork.Commit();
        }

        public void AddGameToBucket(AddGameToBucketDto game)
        {
            var orderGame = AutoMapper.Mapper.Map<AddGameToBucketDto, OrderGame>(game);
            _unitOfWork.OrderRepository.AddGameToBucket(orderGame);
            _unitOfWork.Commit();
        }

        public void DeleteGameFromBucket(AddGameToBucketDto game)
        {
            var completedOrder = _unitOfWork.OrderRepository.GetCurrentCustomerOrder(game.CustomerId);
            var orderGameId = completedOrder.Games.First(x => x.GameId == game.GameId).OrderGameId;

            _unitOfWork.OrderRepository.DeleteGameFromBucket(orderGameId);
            _unitOfWork.Commit();
        }

        public IEnumerable<GameDto> GetGameFromBucket(string customerId)
        {
            var order = _unitOfWork.OrderRepository.GetCurrentCustomerOrder(customerId);

            if (order == null)
            {
                return Enumerable.Empty<GameDto>();
            }

            var games = order.Games.Select(x => x.Game).ToList();
            return AutoMapper.Mapper.Map<IEnumerable<Game>, List<GameDto>>(games);
        }

        public IEnumerable<GameDto> GetCustomerGames(string customerId)
        {
            var gamesFilterParams = new GamesFilterParams() { CustomerId = customerId, PageSize = int.MaxValue };
            var games = _unitOfWork.GameRepository.GetGamesByParams(gamesFilterParams, out var totalCount);
            return AutoMapper.Mapper.Map<IEnumerable<Game>, List<GameDto>>(games);
        }

        public IEnumerable<OrderDto> GetOrdersByParams(OrdersFilterParams filterParams, out int totalCount)
        {
            var orders = _unitOfWork.OrderRepository.GetOrdersByParams(filterParams, out totalCount);
            return AutoMapper.Mapper.Map<IEnumerable<Order>, List<OrderDto>>(orders);
        }

        public OrderDto GetCurrentOrderOfCustomer(string customerId)
        {
            var order = _unitOfWork.OrderRepository.GetCurrentCustomerOrder(customerId);
            return AutoMapper.Mapper.Map<Order, OrderDto>(order);
        }
    }
}
