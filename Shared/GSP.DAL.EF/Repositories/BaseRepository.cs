using GSP.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.DAL.EF.Repositories
{
    public class BaseRepository<TId, TEntity, TContext> : IGenericRepository<TId, TEntity>
        where TEntity : class, new()
        where TContext : DbContext
    {
        public BaseRepository(TContext context)
        {
            DbContext = context;
        }

        protected TContext DbContext { get; }

        public virtual void Create(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);
        }

        public virtual void Delete(TId id)
        {
            TEntity entity = DbContext.Set<TEntity>().Find(id);
            DbContext.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<TEntity> GetAsync(TId id, CancellationToken ct = default)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetListAsync(CancellationToken ct = default)
        {
            return await DbContext.Set<TEntity>().AsNoTracking().ToListAsync(ct);
        }
    }
}