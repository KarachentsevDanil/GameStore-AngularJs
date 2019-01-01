using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Domain.Params;
using GSP.Order.BLL.DTOs.Order;
using GSP.Order.BLL.Services.Contracts;
using GSP.Order.Domain.Enums;
using GSP.Order.Domain.Orders;
using GSP.Order.Domain.Params;
using Microsoft.Extensions.Logging;

namespace GSP.Order.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<OrderService> _logger;

        public OrderService(
            IOrderUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<OrderService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddOrderAsync(CreateOrderDto order, CancellationToken ct = default)
        {
            var newOrder = _mapper.Map<OrderBase>(order);

            _unitOfWork.OrderRepository.Create(newOrder);

            await _unitOfWork.CommitAsync(ct);

            return newOrder.OrderId;
        }

        public async Task UpdateOrderAsync(CompleteOrderDto order, CancellationToken ct = default)
        {
            var completedOrder = await _unitOfWork.OrderRepository.GetCurrentCustomerOrderAsync(order.CustomerId, ct);

            completedOrder.SaleDate = DateTime.Now;
            completedOrder.Status = OrderStatus.Complete;
            completedOrder.PaymentId = order.PaymentId;

            _unitOfWork.OrderRepository.Update(completedOrder);
            await _unitOfWork.CommitAsync(ct);
        }

        public async Task DeleteOrderAsync(int orderId, CancellationToken ct = default)
        {
            _unitOfWork.OrderRepository.Delete(orderId);
            await _unitOfWork.CommitAsync(ct);
        }

        public async Task AddGameToBucketAsync(AddGameToBucketDto game, CancellationToken ct = default)
        {
            var orderGame = _mapper.Map<OrderGame>(game);

            await _unitOfWork.OrderRepository.AddGameToBucketAsync(orderGame, ct);

            await _unitOfWork.CommitAsync(ct);
        }

        public async Task DeleteGameFromBucketAsync(AddGameToBucketDto game, CancellationToken ct = default)
        {
            var completedOrder = await _unitOfWork.OrderRepository.GetCurrentCustomerOrderAsync(game.CustomerId, ct);
            var orderGameId = completedOrder.Games.First(x => x.GameId == game.GameId).OrderGameId;

            await _unitOfWork.OrderRepository.DeleteGameFromBucketAsync(orderGameId, ct);

            await _unitOfWork.CommitAsync(ct);

        }

        public async Task<IEnumerable<OrderGameDto>> GetGameFromBucketAsync(string customerId, CancellationToken ct = default)
        {
            var order = await _unitOfWork.OrderRepository.GetCurrentCustomerOrderAsync(customerId, ct);

            if (order == null)
            {
                return Enumerable.Empty<OrderGameDto>();
            }

            var games = _mapper.Map<List<OrderGameDto>>(order.Games);
            return games;
        }
        
        public async Task<CollectionResult<OrderDto>> GetOrdersByParamsAsync(OrdersFilterParams filterParams, CancellationToken ct = default)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersByParamsAsync(filterParams, ct);
            var result = _mapper.Map<CollectionResult<OrderDto>>(orders);

            return result;
        }

        public async Task<OrderDto> GetCurrentOrderOfCustomerAsync(string customerId, CancellationToken ct = default)
        {
            var order = await _unitOfWork.OrderRepository.GetCurrentCustomerOrderAsync(customerId, ct);
            var orderDto = _mapper.Map<OrderDto>(order);

            return orderDto;
        }
    }
}