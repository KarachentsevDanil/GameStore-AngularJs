using System;
using System.Collections.Generic;
using GSP.BLL.Dto.Game;
using GSP.BLL.Dto.Payment;
using GSP.Domain.Orders;

namespace GSP.BLL.Dto.Order
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string SaleDate { get; set; }

        public OrderStatus Status { get; set; }

        public List<GameDto> Games { get; set; }
        
        public PaymentDto Payment { get; set; }

        public OrderDto()
        {
            Games = new List<GameDto>();
        }
    }
}
