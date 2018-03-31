using System;
using System.Collections.Generic;
using GSP.Orders.Domain.Payments;

namespace GSP.Orders.Domain.Orders
{
    public class Order
    {
        public int OrderId { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int? PaymentId { get; set; }

        public DateTime SaleDate { get; set; }

        public bool IsDeleted { get; set; }

        public OrderStatus Status { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual ICollection<OrderGame> Games { get; set; }

        public Order()
        {
            Games = new List<OrderGame>();
        }
    }
}
