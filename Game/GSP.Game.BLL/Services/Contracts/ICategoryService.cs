using GSP.Game.BLL.DTOs.Category;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Game.BLL.Services.Contracts
{
    public interface ICategoryService
    {
        Task AddCategoryAsync(CreateCategoryDto category, CancellationToken ct = default);

        Task<CategoryDto> GetCategoryByNameAsync(string name, CancellationToken ct = default);

        Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CancellationToken ct = default);
    }
}