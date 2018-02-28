using System.Collections.Generic;
using GSP.BLL.Dto.Category;
using GSP.BLL.Services.Cache;
using GSP.BLL.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSP.SPA.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICacheService _cacheService;

        public CategoryController(ICategoryService categoryService, ICacheService cacheService)
        {
            _categoryService = categoryService;
            _cacheService = cacheService;
        }

        [HttpGet]
        public IEnumerable<CategoryDto> GetCategories()
        {
            var categories = _cacheService.Get<IEnumerable<CategoryDto>>(CacheKey.Categories, CacheBucket.Categories);

            if (categories == null)
            {
                categories = _categoryService.GetCategories();
                _cacheService.Add(categories, CacheKey.Categories, CacheBucket.Categories);
            }

            return categories;
        }

        [HttpPost]
        public void AddCategory([FromBody]CreateCategoryDto category)
        {
            _categoryService.AddCategory(category);
            _cacheService.ResetCategories();
        }
    }
}
