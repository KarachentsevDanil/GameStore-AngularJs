namespace GSP.Payment.Domain.Payments
{
    public class PaymentBase
    {
        public int PaymentId { get; set; }

        public string CustomerId { get; set; }

        public string CreditCard { get; set; }

        public string CvvCode { get; set; }

        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }

        public string FullName { get; set; }

        public string Country { get; set; }
    }
}