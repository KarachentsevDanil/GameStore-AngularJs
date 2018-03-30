using System.Collections.Generic;
using GSP.Domain.Payments;

namespace GSP.DAL.Repositories.Contracts
{
    public interface IPaymentRepository : IGameStoreRepository<Payment>
    {
        IEnumerable<Payment> GetCustomerPayments(string customerId);
    }
}
