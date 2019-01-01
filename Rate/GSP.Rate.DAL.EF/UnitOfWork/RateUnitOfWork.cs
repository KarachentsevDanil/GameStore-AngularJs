using GSP.DAL.EF.Context;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Rate.DAL.Repositories.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.DAL.EF.UnitOfWork
{
    public class RateUnitOfWork : IRateUnitOfWork
    {
        private readonly RateContext _context;

        public RateUnitOfWork(RateContext context)
        {
            _context = context;
        }

        public IRateRepository RateRepository { get; set; }

        public async Task CommitAsync(CancellationToken ct= default)
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