using System;
using System.Collections.Generic;
using GSP.Domain.Customers;
using Newtonsoft.Json;

namespace GSP.Domain.Orders
{
    [JsonObject(IsReference = true)]
    public class Order
    {
        public int OrderId { get; set; }

        public string CustomerId { get; set; }

        public DateTime SaleDate { get; set; }

        public bool IsDeleted { get; set; }

        public OrderStatus Status { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderGame> Games { get; set; }

        public Order()
        {
            Games = new List<OrderGame>();
        }
    }
}
