using System.Collections.Generic;
using GSP.Orders.BLL.Dto.Payment;
using GSP.Orders.Domain.Orders;

namespace GSP.Orders.BLL.Dto.Order
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
