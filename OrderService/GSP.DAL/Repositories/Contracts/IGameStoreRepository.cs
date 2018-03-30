using System.Linq;

namespace GSP.DAL.Repositories.Contracts
{
    public interface IGameStoreRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
