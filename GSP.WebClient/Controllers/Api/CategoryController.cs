//using System.Collections.Generic;
//using GSP.BLL.Services.Contracts;
//using GSP.Domain.Games;
//using Microsoft.AspNetCore.Mvc;

//namespace GSP.WebClient.Controllers.Api
//{
//    [Route("api/[controller]/[action]")]
//    public class CategoryController : Controller
//    {
//        private readonly ICategoryService _categoryService;

//        public CategoryController(ICategoryService categoryService)
//        {
//            _categoryService = categoryService;
//        }

//        [HttpGet]
//        public IEnumerable<Category> GetCategories()
//        {
//            return _categoryService.GetCategories();
//        }

//        [HttpPost]
//        public void AddCategory([FromBody]Category category)
//        {
//            _categoryService.AddCategory(category.Name);
//        }
//    }
//}
