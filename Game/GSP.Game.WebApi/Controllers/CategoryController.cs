using GSP.BLL.Constants;
using GSP.BLL.Enums;
using GSP.BLL.Services.Contracts;
using GSP.Game.BLL.DTOs.Category;
using GSP.Game.BLL.Services.Contracts;
using GSP.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSP.Game.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        private readonly ICacheService _cacheService;

        public CategoryController(
            ICategoryService categoryService,
            ICacheService cacheService)
        {
            _categoryService = categoryService;
            _cacheService = cacheService;
        }

        [HttpGet]
        public async Task<JsonResultData> GetCategories()
        {
            var categories = _cacheService.Get<IEnumerable<CategoryDto>>(CacheKey.Categories, CacheBucket.Categories);

            if (categories == null)
            {
                categories = await _categoryService.GetCategoriesAsync();
                _cacheService.Add(categories, CacheKey.Categories, CacheBucket.Categories);
            }

            return JsonResultData.Success(categories);
        }

        [HttpPost]
        public async Task<JsonResultData> AddCategory([FromBody]CreateCategoryDto category)
        {
            await _categoryService.AddCategoryAsync(category);

            _cacheService.ResetCategories();

            return JsonResultData.Success();
        }
    }
}