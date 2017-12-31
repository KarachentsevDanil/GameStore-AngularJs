using System.Collections.Generic;
using GSP.BLL.Services.Contracts;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Domain.Games;

namespace GSP.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGameStoreUnitOfWork _unitOfWork;

        public CategoryService(IGameStoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddCategory(string name)
        {
            _unitOfWork.CategoryRepository.Add(new Category(name));
            _unitOfWork.Commit();
        }

        public Category GetCategoryByName(string name)
        {
            return _unitOfWork.CategoryRepository.GetCategoryByName(name);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.CategoryRepository.GetCategories();
        }
    }
}
