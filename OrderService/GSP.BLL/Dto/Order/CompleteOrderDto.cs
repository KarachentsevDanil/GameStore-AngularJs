using System;
using GSP.Domain.Orders;

namespace GSP.BLL.Dto.Order
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
