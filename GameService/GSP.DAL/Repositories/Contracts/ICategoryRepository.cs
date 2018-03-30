using System.Collections.Generic;
using GSP.Common.DAL.Repositories.Contracts;
using GSP.Games.Domain.Games;

namespace GSP.Game.DAL.Repositories.Contracts
{
    public interface ICategoryRepository : IGameStoreRepository<Category>
    {
        Category GetCategoryByName(string name);

        IEnumerable<Category> GetCategories();
    }
}
