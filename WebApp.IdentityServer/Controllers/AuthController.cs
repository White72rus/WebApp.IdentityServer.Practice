using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.IdentityServer.ViewModel;

namespace WebApp.IdentityServer.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.User);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "User not found.");
                return View(model);
            }

            var signin = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (signin.Succeeded)
                return Redirect(model.ReturnUrl);

            return View(model);
        }
    }
}
