using GSP.Account.BLL.DTOs.Customer;
using GSP.Account.BLL.Services.Contracts;
using GSP.Account.Domain.Customers;
using GSP.Account.WebApi.Models;
using GSP.WebApi.Configurations;
using GSP.WebApi.Extensions;
using GSP.WebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GSP.Account.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Customer> _userManager;

        private readonly SignInManager<Customer> _signInManager;

        private readonly AuthenticationConfiguration _configuration;

        private readonly ICustomerService _customerService;

        public AccountController(
            UserManager<Customer> userManager,
            SignInManager<Customer> signInManager,
            AuthenticationConfiguration configuration,
            ICustomerService customerService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _customerService = customerService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<JsonResultData> Login([FromBody] CustomerLoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _customerService.GetCustomerByTermAsync(model.Email);
                var token = GenerateToken(user);

                var tokenResponse = new TokenResponseModel
                {
                    Customer = user,
                    AccessToken = token,
                    ExpiresIn = (int) TimeSpan.FromDays(1).TotalSeconds
                };

                return JsonResultData.Success(tokenResponse);
            }

            return JsonResultData.Error("Username or password isn't correct.");
        }

        [HttpPost]
        [Route("register")]
        public async Task<JsonResultData> Register([FromBody] CustomerRegistrationDto data)
        {
            if (ModelState.IsValid)
            {
                var user = new Customer
                {
                    UserName = data.Email,
                    Email = data.Email,
                    FullName = data.FullName,
                    DateOfBirthday = data.DateOfBirthday
                };

                var result = await _userManager.CreateAsync(user, data.Password);

                if (result.Succeeded)
                {
                    return JsonResultData.Success();
                }
            }

            return JsonResultData.Error("User already exists.");
        }

        private string GenerateToken(CustomerDto customer)
        {
            var claims = new[]
            {
                new Claim(nameof(Customer.Id), customer.CustomerId),
                new Claim(nameof(Customer.Email), customer.Email),
                new Claim(ClaimTypes.Role, customer.Role),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(_configuration.GetSymmetricSecurityKey(),SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}