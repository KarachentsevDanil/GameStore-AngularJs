using System.Collections.Generic;
using GSP.BLL.Dto.Rate;
using GSP.BLL.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSP.SPA.Controllers.Api
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
        public IEnumerable<RateDto> GetGameRates(int id)
        {
            var rates = _rateService.GetRatesOfGame(id);
            return rates;
        }

        [HttpPost]
        public void CreateFeed([FromBody] CreateRateDto rateViewModel)
        {
            _rateService.AddFeedbackToGame(rateViewModel);
        }
    }
}
