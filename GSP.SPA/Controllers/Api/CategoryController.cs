using System.Collections.Generic;
using GSP.BLL.Dto.Category;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Games;
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

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IEnumerable<CategoryDto> GetCategories()
        {
            return _categoryService.GetCategories();
        }

        [HttpPost]
        public void AddCategory([FromBody]CreateCategoryDto category)
        {
            _categoryService.AddCategory(category);
        }
    }
}
