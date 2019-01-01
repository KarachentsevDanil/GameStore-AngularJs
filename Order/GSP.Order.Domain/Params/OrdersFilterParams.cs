using GSP.Domain.Params;
using GSP.Order.Domain.Orders;

namespace GSP.Order.Domain.Params
{
    public class OrdersFilterParams : FilterParams<OrderBase>
    {
        public string Customer { get; set; }

        public string CustomerId { get; set; }
    }
}