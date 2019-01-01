using GSP.DAL.EF.Context;
using GSP.DAL.Repositories.Contracts;
using GSP.Domain.Payments;
using System.Collections.Generic;
using System.Linq;

namespace GSP.DAL.EF.Repositories
{
    public class PaymentRepository : GameStoreRepository<Payment>, IPaymentRepository
    {
        private readonly GameStoreContext _dbContext;

        public PaymentRepository(GameStoreContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Payment> GetCustomerPayments(string customerId)
        {
            return _dbContext.Payments.Where(p => p.CustomerId.ToUpper() == customerId.ToUpper());
        }
    }
}
