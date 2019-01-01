using GSP.DAL.Repositories.Contracts;
using GSP.Payment.Domain.Payments;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Payment.DAL.Repositories.Contracts
{
    public interface IPaymentRepository : IGenericRepository<int, PaymentBase>
    {
        Task<IEnumerable<PaymentBase>> GetCustomerPaymentsAsync(string customerId, CancellationToken ct = default);
    }
}