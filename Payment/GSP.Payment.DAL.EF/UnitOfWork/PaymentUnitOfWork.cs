using GSP.Payment.DAL.EF.Context;
using GSP.Payment.DAL.Repositories.Contracts;
using GSP.Payment.DAL.UnitOfWork.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Payment.DAL.EF.UnitOfWork
{
    public class PaymentUnitOfWork : IPaymentUnitOfWork
    {
        private readonly PaymentContext _context;

        public PaymentUnitOfWork(PaymentContext context)
        {
            _context = context;
        }

        public IPaymentRepository PaymentRepository { get; set; }

        public async Task CommitAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}