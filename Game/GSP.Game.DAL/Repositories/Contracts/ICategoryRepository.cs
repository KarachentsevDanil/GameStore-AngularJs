using GSP.DAL.Repositories.Contracts;
using GSP.Game.Domain.Games;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Game.DAL.Repositories.Contracts
{
    public interface ICategoryRepository : IGenericRepository<int, Category>
    {
        Task<Category> GetCategoryByNameAsync(string name, CancellationToken ct = default);

        Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken ct = default);
    }
}