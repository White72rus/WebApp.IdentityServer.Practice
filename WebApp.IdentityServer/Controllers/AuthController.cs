using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.IdentityServer.Infrastructure;
using WebApp.IdentityServer.Models;
using WebApp.IdentityServer.ViewModel;

namespace WebApp.IdentityServer.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserService _userService;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, UserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        [EnableCors("MyCorsPolicy")]
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

            _userService.Add(new User()
            {
                Name = user.UserName,
                Id = user.Id,
                Email = user.Email,
            });

            var signin = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (signin.Succeeded)
                return Redirect(model.ReturnUrl);

            return View(model.ReturnUrl);
        }
    }
}
