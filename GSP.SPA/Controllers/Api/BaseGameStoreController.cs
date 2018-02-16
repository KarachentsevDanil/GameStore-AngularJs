using GSP.BLL.Dto.Customer;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Customers;
using Microsoft.AspNetCore.Mvc;

namespace GSP.SPA.Controllers.Api
{
    public class BaseGameStoreController : Controller
    {
        private readonly ICustomerService _customerService;

        public BaseGameStoreController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        protected CustomerDto GetCustomerByTerm(string term)
        {
            return _customerService.GetCustomerByTerm(term);
        }
    }
}
