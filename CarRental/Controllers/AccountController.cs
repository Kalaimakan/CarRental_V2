using CarRental.Interfaces;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarRental.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;

        public AccountController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult CustomerLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CustomerLogin(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid)
                return View(loginVm);

            var customer = await _customerService.AuthenticateAsync(loginVm);

            if (customer == null)
            {
                ModelState.AddModelError("", "Invalid username/email or password");
                return View(loginVm);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, customer.Name),                  // Display Name
                new Claim(ClaimTypes.Email, customer.Email),                // Email
                new Claim("CustomerId", customer.Id.ToString())             // Custom ID claim
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "CustomerDashboard");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("CustomerLogin");
        }
    }
}
