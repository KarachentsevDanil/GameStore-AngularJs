using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GSP.Payment.BLL.DTOs.Payment;

namespace GSP.Payment.BLL.Services.Contracts
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetCustomerPaymentsAsync(string customerId, CancellationToken ct = default);

        Task<int> AddPaymentAsync(AddPaymentDto payment, CancellationToken ct = default);
    }
}