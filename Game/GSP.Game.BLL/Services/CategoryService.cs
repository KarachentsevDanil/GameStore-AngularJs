using AutoMapper;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Game.BLL.DTOs.Category;
using GSP.Game.BLL.Services.Contracts;
using GSP.Game.Domain.Games;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Game.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGameUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<CategoryService> _logger;

        public CategoryService(
            IGameUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddCategoryAsync(CreateCategoryDto category, CancellationToken ct = default)
        {
            var newCategory = _mapper.Map<Category>(category);
            _unitOfWork.CategoryRepository.Create(newCategory);

            await _unitOfWork.CommitAsync(ct);
        }

        public async Task<CategoryDto> GetCategoryByNameAsync(string name, CancellationToken ct = default)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategoryByNameAsync(name, ct);
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CancellationToken ct = default)
        {
            var categories = await _unitOfWork.CategoryRepository.GetCategoriesAsync(ct);
            var categoryListDto =  _mapper.Map<List<CategoryDto>>(categories);

            return categoryListDto;
        }
    }
}