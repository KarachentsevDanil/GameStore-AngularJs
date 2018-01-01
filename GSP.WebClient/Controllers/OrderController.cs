using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSP.WebClient.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        [HttpGet]
        public IActionResult Bucket()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Order()
        {
            return View();
        }
    }
}