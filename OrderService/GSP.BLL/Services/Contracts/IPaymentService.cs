using System.Collections.Generic;
using GSP.Orders.BLL.Dto.Payment;

namespace GSP.Orders.BLL.Services.Contracts
{
    public interface IPaymentService
    {
        IEnumerable<PaymentDto> GetCustomerPayments(string customerId);

        int AddPayment(AddPaymentDto payment);
    }
}
