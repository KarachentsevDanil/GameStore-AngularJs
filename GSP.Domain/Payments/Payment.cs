using System.Collections.Generic;
using GSP.Domain.Customers;
using GSP.Domain.Orders;

namespace GSP.Domain.Payments
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public string CustomerId { get; set; }

        public string CreditCard { get; set; }

        public string CvvCode { get; set; }

        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }

        public string FullName { get; set; }

        public string Country { get; set; }

        public virtual Customer Customer { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
