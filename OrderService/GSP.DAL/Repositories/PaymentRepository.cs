using System.Collections.Generic;
using System.Linq;
using GSP.Common.DAL.Repositories;
using GSP.Orders.DAL.Context;
using GSP.Orders.DAL.Repositories.Contracts;
using GSP.Orders.Domain.Payments;

namespace GSP.Orders.DAL.Repositories
{
    public class PaymentRepository : GameStoreRepository<Payment, GameStoreOrderContext>, IPaymentRepository
    {
        private readonly GameStoreOrderContext _dbContext;

        public PaymentRepository(GameStoreOrderContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Payment> GetCustomerPayments(string customerId)
        {
            return _dbContext.Payments.Where(p => p.CustomerId.ToUpper() == customerId.ToUpper());
        }
    }
}
