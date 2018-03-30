using System.Collections.Generic;
using GSP.BLL.Dto.Category;
using GSP.BLL.Services.Contracts;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Games.Domain.Games;

namespace GSP.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGameStoreUnitOfWork _unitOfWork;

        public CategoryService(IGameStoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddCategory(CreateCategoryDto category)
        {
            var newCategory = AutoMapper.Mapper.Map<CreateCategoryDto, Category>(category);
            _unitOfWork.CategoryRepository.Add(newCategory);
            _unitOfWork.Commit();
        }

        public CategoryDto GetCategoryByName(string name)
        {
            var category = _unitOfWork.CategoryRepository.GetCategoryByName(name);
            return AutoMapper.Mapper.Map<Category, CategoryDto>(category);
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            var categories = _unitOfWork.CategoryRepository.GetCategories();
            return AutoMapper.Mapper.Map<IEnumerable<Category>, List<CategoryDto>>(categories);
        }
    }
}
