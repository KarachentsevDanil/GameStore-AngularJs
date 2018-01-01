using GSP.BLL.Services.Contracts;
using GSP.Domain.Customers;
using Microsoft.AspNetCore.Mvc;


namespace GSP.WebClient.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;

        public AccountController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public void CreateCustomer([FromBody] Customer customer)
        {
            _customerService.AddCustomer(customer);
        }
    }
}
