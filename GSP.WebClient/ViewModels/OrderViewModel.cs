using GSP.Domain.Orders;

namespace GSP.WebClient.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public string Customer { get; set; }
    }
}
