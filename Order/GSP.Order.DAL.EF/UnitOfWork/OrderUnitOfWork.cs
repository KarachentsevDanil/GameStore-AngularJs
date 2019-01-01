using GSP.DAL.EF.Context;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Order.DAL.Repositories.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;
using GSP.Order.DAL.EF.Context;

namespace GSP.DAL.EF.UnitOfWork
{
    public class OrderUnitOfWork : IOrderUnitOfWork
    {
        private readonly OrderContext _context;

        public OrderUnitOfWork(OrderContext context)
        {
            _context = context;
        }

        public IOrderRepository OrderRepository { get; set; }

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