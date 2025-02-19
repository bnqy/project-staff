using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using project_staff.Services.WebApi;
using project_staff.Shared.DTOs;
using System.Security.Claims;

namespace project_staff.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountApiClient _accountApiClient;

        public AccountController(AccountApiClient accountApiClient)
        {
            _accountApiClient = accountApiClient;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUserForRegistrationDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _accountApiClient.RegisterAsync(model);
            if (success)
            {
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "Registration failed. Please try again.");
            return View(model);
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(ApplicationUserForAuthenticationDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var token = await _accountApiClient.LoginAsync(model);

            if (!string.IsNullOrEmpty(token))
            {
                Response.Cookies.Append("JWToken", token);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim("JWT", token)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Projects");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }
    }
}
