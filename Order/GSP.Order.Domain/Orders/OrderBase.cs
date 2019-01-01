using GSP.Order.Domain.Enums;
using System;
using System.Collections.Generic;

namespace GSP.Order.Domain.Orders
{
    public class OrderBase
    {
        public OrderBase()
        {
            Games = new List<OrderGame>();
        }

        public int OrderId { get; set; }

        public string CustomerId { get; set; }

        public int? PaymentId { get; set; }

        public DateTime SaleDate { get; set; }

        public bool IsDeleted { get; set; }

        public OrderStatus Status { get; set; }

        public ICollection<OrderGame> Games { get; set; }
    }
}