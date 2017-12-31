using System;
using GSP.DAL.Context;
using GSP.DAL.Repositories.Contracts;
using GSP.DAL.UnitOfWork.Contracts;

namespace GSP.DAL.UnitOfWork
{
    public class GameStoreUnitOfWork : IGameStoreUnitOfWork, IDisposable
    {
        private readonly GameStoreContext _context;

        public GameStoreUnitOfWork(GameStoreContext context)
        {
            _context = context;
        }

        public IGameRepository GameRepository { get; set; }

        public IOrderRepository OrderRepository { get; set; }

        public ICustomerRepository CustomerRepository { get; set; }

        public IRateRepository RateRepository { get; set; }

        public ICategoryRepository CategoryRepository { get; set; }

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
