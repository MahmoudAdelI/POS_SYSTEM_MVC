using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.ViewModels;

namespace POS_SYSTEM_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
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
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    var result =
                        await _signInManager.PasswordSignInAsync(
                            user,
                            model.Password,
                            false,
                            false);

                    if (result.Succeeded)
                    {
                        var roles =
                            await _userManager.GetRolesAsync(user);

                        if (roles.Contains("admin"))
                        {
                            return RedirectToAction(
                                "Index",
                                "Dashboard",
                                new { area = "Admin" });
                            //return Redirect("/Admin/Dashboard");
                        }

                        return RedirectToAction(
                            "Index",
                            "Cashier");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Login Failed");

                    }
                }

                ModelState.AddModelError(
                    "",
                    "Invalid Username or Password");
            }

            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
