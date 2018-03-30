using System.Collections.Generic;
using GSP.BLL.Dto.Category;

namespace GSP.BLL.Services.Contracts
{
    public interface ICategoryService
    {
        void AddCategory(CreateCategoryDto category);

        CategoryDto GetCategoryByName(string name);

        IEnumerable<CategoryDto> GetCategories();
    }
}