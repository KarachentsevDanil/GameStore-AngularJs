using GSP.DAL.EF.Context;
using GSP.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GSP.DAL.EF.Repositories
{
    public class GameStoreRepository<TEntity> : IGameStoreRepository<TEntity> where TEntity : class
    {
        public GameStoreRepository(GameStoreContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        protected GameStoreContext DbContext { get; set; }

        protected DbSet<TEntity> DbSet { get; set; }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual TEntity GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
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
