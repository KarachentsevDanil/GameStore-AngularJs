using GSP.DAL.EF.Context;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Game.DAL.Repositories.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.DAL.EF.UnitOfWork
{
    public class GameUnitOfWork : IGameUnitOfWork
    {
        private readonly GameContext _context;

        public GameUnitOfWork(GameContext context)
        {
            _context = context;
        }

        public IGameRepository GameRepository { get; set; }

        public ICategoryRepository CategoryRepository { get; set; }

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