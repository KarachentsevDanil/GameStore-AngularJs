using System.Collections.Generic;
using GSP.Common.BLL.Services.Cache;
using GSP.Common.Web.Models;
using GSP.Orders.BLL.Dto.Payment;
using GSP.Orders.BLL.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSP.Orders.WebService.ApiControllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderCacheService _cacheService;

        public PaymentController(IPaymentService paymentService, IOrderCacheService cacheService)
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
