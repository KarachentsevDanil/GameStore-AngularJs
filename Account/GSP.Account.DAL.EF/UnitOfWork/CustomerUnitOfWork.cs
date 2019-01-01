using GSP.Account.DAL.EF.Context;
using GSP.Account.DAL.Repositories.Contracts;
using GSP.Account.DAL.UnitOfWork.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Account.DAL.EF.UnitOfWork
{
    public class CustomerUnitOfWork : ICustomerUnitOfWork
    {
        private readonly CustomerContext _context;

        public CustomerUnitOfWork(CustomerContext context)
        {
            _context = context;
        }
        
        public ICustomerRepository CustomerRepository { get; set; }
        
        public async Task CommitAsync(CancellationToken token = default)
        {
            await _context.SaveChangesAsync(token);
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