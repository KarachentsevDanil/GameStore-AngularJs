using GSP.Rate.BLL.DTOs.Rate;
using GSP.Rate.BLL.Services.Contracts;
using GSP.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GSP.Rate.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRateService _rateService;

        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        [HttpGet("{id}")]
        public async Task<JsonResultData> GetGameRates(int id)
        {
            var rates = await _rateService.GetRatesOfGameAsync(id);
            return JsonResultData.Success(rates);
        }

        [HttpPost]
        public async Task<JsonResultData> CreateFeed([FromBody] CreateRateDto rateViewModel)
        {
            await _rateService.AddFeedbackToGameAsync(rateViewModel);
            return JsonResultData.Success();
        }
    }
}