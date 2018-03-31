using System;
using GSP.Orders.Domain.Orders;

namespace GSP.Orders.BLL.Dto.Order
{
    public class CompleteOrderDto
    {
        public int OrderId { get; set; }

        public string CustomerId { get; set; }

        public DateTime SaleDate { get; set; }

        public OrderStatus Status { get; set; }

        public int PaymentId { get; set; }
    }
}
