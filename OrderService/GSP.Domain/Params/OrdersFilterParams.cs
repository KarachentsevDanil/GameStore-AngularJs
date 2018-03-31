using GSP.Common.Domain.Params;
using GSP.Orders.Domain.Orders;

namespace GSP.Orders.Domain.Params
{
    public class OrdersFilterParams : FilterParams<Order>
    {
        public string Customer { get; set; }

        public string CustomerId { get; set; }
    }
}
