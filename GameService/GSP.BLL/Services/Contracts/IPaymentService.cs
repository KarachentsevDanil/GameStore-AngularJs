using System.Collections.Generic;
using GSP.BLL.Dto.Payment;

namespace GSP.BLL.Services.Contracts
{
    public interface IPaymentService
    {
        IEnumerable<PaymentDto> GetCustomerPayments(string customerId);

        int AddPayment(AddPaymentDto payment);
    }
}
