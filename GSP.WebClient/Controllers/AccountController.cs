using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using GSP.BLL.Resources;
using GSP.BLL.Services.Contracts;
using GSP.Domain.Customers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSP.WebClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;

        public AccountController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var user = _customerService.GetCustomerByTerm(customer.Email);

                if (user != null)
                {
                    await Authenticate(user);

                    return RedirectToAction(User.IsInRole(Role.Admin.ToString()) ? "EditOrDeleteGame" : "ShowGame", "Game");
                }

                ModelState.AddModelError(string.Empty, Exceptions.IncorrectPasswordOrUserName);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Customer customer)
        {
            if (ModelState.IsValid)
            {

                var user = _customerService.GetCustomerByTerm(customer.Email);
                if (user == null)
                {
                    _customerService.AddCustomer(customer);

                    await Authenticate(customer);

                    return RedirectToAction(User.IsInRole(Role.Admin.ToString()) ? "EditOrDeleteGame" : "ShowGame", "Game");
                }

                ModelState.AddModelError(string.Empty, Exceptions.UserAlreadyExists);
            }

            return View();
        }

        private async Task Authenticate(Customer customer)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, customer.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, customer.Role.ToString())
            };
            
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
