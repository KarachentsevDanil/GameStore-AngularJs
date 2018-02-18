﻿using GSP.Domain.Orders;

namespace GSP.Domain.Params
{
    public class OrdersFilterParams : FilterParams<Order>
    {
        public string Customer { get; set; }

        public string CustomerId { get; set; }
    }
}
