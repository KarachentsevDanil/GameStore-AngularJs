using System;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Game.DAL.Context;
using GSP.Game.DAL.Repositories.Contracts;

namespace GSP.DAL.UnitOfWork
{
    public class GameStoreUnitOfWork : IGameStoreUnitOfWork, IDisposable
    {
        private readonly GameStoreGameContext _context;

        public GameStoreUnitOfWork(GameStoreGameContext context)
        {
            _context = context;
        }

        public IGameRepository GameRepository { get; set; }

        public IOrderRepository OrderRepository { get; set; }

        public ICustomerRepository CustomerRepository { get; set; }

        public IRateRepository RateRepository { get; set; }

        public ICategoryRepository CategoryRepository { get; set; }

        public IPaymentRepository PaymentRepository { get; set; }

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
