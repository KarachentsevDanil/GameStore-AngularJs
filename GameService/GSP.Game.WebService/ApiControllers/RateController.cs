using GSP.Common.Web.Models;
using GSP.Games.BLL.Dto.Rate;
using GSP.Games.BLL.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSP.Games.WebService.ApiControllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RateController : Controller
    {
        private readonly IRateService _rateService;

        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        [HttpGet]
        public IActionResult GetGameRates(int id)
        {
            var rates = _rateService.GetRatesOfGame(id);
            return Json(JsonResultData.Success(rates));
        }

        [HttpPost]
        public IActionResult CreateFeed([FromBody] CreateRateDto rateViewModel)
        {
            _rateService.AddFeedbackToGame(rateViewModel);
            return Json(JsonResultData.Success());
        }
    }
}
