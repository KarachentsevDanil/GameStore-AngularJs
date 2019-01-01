using System.Collections.Generic;
using GSP.Order.Domain.Enums;

namespace GSP.Order.BLL.DTOs.Order
{
    public class OrderDto
    {
        public OrderDto()
        {
            Games = new List<OrderGameDto>();
        }

        public int OrderId { get; set; }

        public string CustomerId { get; set; }

        public int PaymentId { get; set; }

        public string CustomerName { get; set; }

        public string SaleDate { get; set; }

        public OrderStatus Status { get; set; }

        public List<OrderGameDto> Games { get; set; }
    }
}