using System.Collections.Generic;
using GSP.Common.BLL.Services.Cache;
using GSP.Common.BLL.Services.Contracts;
using GSP.Common.Web.Models;
using GSP.Games.BLL.Dto.Category;
using GSP.Games.BLL.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSP.Games.WebService.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IGameCacheService _cacheService;

        public CategoryController(ICategoryService categoryService, IGameCacheService cacheService)
        {
            _categoryService = categoryService;
            _cacheService = cacheService;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _cacheService.Get<IEnumerable<CategoryDto>>(CacheKey.Categories, CacheBucket.Categories);

            if (categories == null)
            {
                categories = _categoryService.GetCategories();
                _cacheService.Add(categories, CacheKey.Categories, CacheBucket.Categories);
            }
            
            return Json(JsonResultData.Success(categories));
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody]CreateCategoryDto category)
        {
            _categoryService.AddCategory(category);
            _cacheService.ResetCategories();

            return Json(JsonResultData.Success());
        }
    }
}
