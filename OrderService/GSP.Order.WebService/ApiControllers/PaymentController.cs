using System.Collections.Generic;
using GSP.BLL.Dto.Payment;
using GSP.BLL.Services.Cache;
using GSP.BLL.Services.Contracts;
using GSP.SPA.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSP.SPA.Controllers.Api
{
    [Route("api/[controller]/[action]/{id?}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly ICacheService _cacheService;

        public PaymentController(IPaymentService paymentService, ICacheService cacheService)
        {
            _paymentService = paymentService;
            _cacheService = cacheService;
        }

        [HttpGet]
        public IActionResult GetCustomerPayments(string id)
        {
            var payments = _cacheService.Get<IEnumerable<PaymentDto>>($"{CacheKey.Payments}_{id}", CacheBucket.Payments);

            if (payments == null)
            {
                payments = _paymentService.GetCustomerPayments(id);
                _cacheService.Add(payments, $"{CacheKey.Payments}_{id}", CacheBucket.Payments);
            }

            return Json(JsonResultData.Success(payments));
        }

        [HttpPost]
        public IActionResult AddPayment([FromBody] AddPaymentDto payment)
        {
            _cacheService.ResetPayments($"{CacheKey.Payments}_{payment.CustomerId}");
            var paymentId = _paymentService.AddPayment(payment);

            return Json(JsonResultData.Success(paymentId));
        }
    }
}
