using System.Collections.Generic;
using GSP.Domain.Games;

namespace GSP.DAL.Repositories.Contracts
{
    public interface ICategoryRepository : IGameStoreRepository<Category>
    {
        Category GetCategoryByName(string name);

        IEnumerable<Category> GetCategories();
    }
}
