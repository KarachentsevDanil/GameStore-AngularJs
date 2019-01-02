using GSP.BLL.Constants;
using GSP.BLL.Enums;
using GSP.BLL.Services.Contracts;
using GSP.Payment.BLL.DTOs.Payment;
using GSP.Payment.BLL.Services.Contracts;
using GSP.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSP.Payment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        private readonly ICacheService _cacheService;

        public PaymentController(
            IPaymentService paymentService,
            ICacheService cacheService)
        {
            _paymentService = paymentService;
            _cacheService = cacheService;
        }

        [HttpGet("{customerId}")]
        public async Task<JsonResultData> Get(string customerId)
        {
            var payments = _cacheService.Get<IEnumerable<PaymentDto>>($"{CacheKey.Payments}_{customerId}", CacheBucket.Payments);

            if (payments == null)
            {
                payments = await _paymentService.GetCustomerPaymentsAsync(customerId);
                _cacheService.Add(payments, $"{CacheKey.Payments}_{customerId}", CacheBucket.Payments);
            }

            return JsonResultData.Success(payments);
        }
        
        [HttpPost]
        public async Task<JsonResultData> AddPayment([FromBody] AddPaymentDto payment)
        {
            var paymentId = await _paymentService.AddPaymentAsync(payment);

            return JsonResultData.Success(paymentId);
        }
    }
}