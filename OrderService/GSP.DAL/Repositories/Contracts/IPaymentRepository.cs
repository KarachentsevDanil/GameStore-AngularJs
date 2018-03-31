using System.Collections.Generic;
using GSP.Common.DAL.Repositories.Contracts;
using GSP.Orders.Domain.Payments;

namespace GSP.Orders.DAL.Repositories.Contracts
{
    public interface IPaymentRepository : IGameStoreRepository<Payment>
    {
        IEnumerable<Payment> GetCustomerPayments(string customerId);
    }
}
