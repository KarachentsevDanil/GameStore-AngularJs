using System.Collections.Generic;
using GSP.Games.BLL.Dto.Category;

namespace GSP.Games.BLL.Services.Contracts
{
    public interface ICategoryService
    {
        void AddCategory(CreateCategoryDto category);

        CategoryDto GetCategoryByName(string name);

        IEnumerable<CategoryDto> GetCategories();
    }
}