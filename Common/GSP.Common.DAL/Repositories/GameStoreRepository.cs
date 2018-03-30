using System.Linq;
using GSP.Common.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GSP.Common.DAL.Repositories
{
    public class GameStoreRepository<T, TContext> : IGameStoreRepository<T> where TContext : DbContext where T : class
    {
        public GameStoreRepository(TContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        protected TContext DbContext { get; set; }

        protected DbSet<T> DbSet { get; set; }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);

            if (entity == null)
            {
                return;
            }

            DbSet.Remove(entity);
        }
    }
}
