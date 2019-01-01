using GSP.DAL.EF.Repositories;
using GSP.Payment.DAL.EF.Context;
using GSP.Payment.DAL.Repositories.Contracts;
using GSP.Payment.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Payment.DAL.EF.Repositories
{
    public class PaymentRepository : BaseRepository<int, PaymentBase, PaymentContext>, IPaymentRepository
    {
        private readonly PaymentContext _dbContext;

        public PaymentRepository(PaymentContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PaymentBase>> GetCustomerPaymentsAsync(string customerId, CancellationToken ct = default)
        {
            return await _dbContext.Payments.Where(p => p.CustomerId.ToUpper() == customerId.ToUpper()).ToListAsync(ct);
        }
    }
}