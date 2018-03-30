using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using GSP.BLL.Dto.Customer;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Customers;
using GSP.SPA.Authentication;
using GSP.SPA.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GSP.SPA.Controllers.Api
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly ICustomerService _customerService;

        public AccountController(
            UserManager<Customer> userManager,
            SignInManager<Customer> signInManager,
            ICustomerService customerService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customerService = customerService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] CustomerLoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var token = GenerateToken(model.Email);
                var user = _customerService.GetCustomerByTerm(model.Email);

                return Json(new { user, token, tokenExpireData = DateTime.Now.AddDays(1) });
            }

            return Json(JsonResultData.Error("Username or password isn't correct."));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] CustomerRegistrationDto data)
        {
            if (ModelState.IsValid)
            {
                var user = new Customer
                {
                    UserName = data.Email,
                    Email = data.Email,
                    FullName = data.FullName,
                    DateOfBirthsday = data.DateOfBirthsday
                };

                var result = await _userManager.CreateAsync(user, data.Password);

                if (result.Succeeded)
                {
                    return Json(JsonResultData.Success());
                }
            }

            return Json(JsonResultData.Error("User already exists."));
        }

        private string GenerateToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    AuthenticationOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
