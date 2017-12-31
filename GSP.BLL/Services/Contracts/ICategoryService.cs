using System.Collections.Generic;
using GSP.Domain.Games;

namespace GSP.BLL.Services.Contracts
{
    public interface ICategoryService
    {
        void AddCategory(string name);

        Category GetCategoryByName(string name);

        IEnumerable<Category> GetCategories();
    }
}
