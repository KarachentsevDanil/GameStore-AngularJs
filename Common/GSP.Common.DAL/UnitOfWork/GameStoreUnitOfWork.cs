using System;
using GSP.Common.DAL.UnitOfWork.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GSP.Common.DAL.UnitOfWork
{
    public class GameStoreUnitOfWork<TContext> : IGameStoreUnitOfWork, IDisposable where TContext : DbContext
    {
        private readonly TContext _context;

        public GameStoreUnitOfWork(TContext context)
        {
            _context = context;
        }
        
        public void Commit()
        {
            _context.SaveChanges();
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
