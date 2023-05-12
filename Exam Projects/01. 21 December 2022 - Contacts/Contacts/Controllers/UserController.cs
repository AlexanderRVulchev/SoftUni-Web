using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    using Contacts.Data.Entities;
    using Contacts.Models;
    using Microsoft.AspNetCore.Identity;

    public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(
            SignInManager<ApplicationUser> _signInManager,
            UserManager<ApplicationUser> _userManager)
        {
            signInManager = _signInManager;
            userManager = _userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("All", "Contacts");
            }
            var model = new RegisterViewModel();
            return View(model);
        }

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user = new()
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "User");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
    }
}
