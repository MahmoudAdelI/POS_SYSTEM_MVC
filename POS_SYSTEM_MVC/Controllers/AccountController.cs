using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.ViewModels;

namespace POS_SYSTEM_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(ILogger<AccountController> logger, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        // GET: /Account/Login --MOW#1

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        // POST: /Account/Login --MOW#2
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
