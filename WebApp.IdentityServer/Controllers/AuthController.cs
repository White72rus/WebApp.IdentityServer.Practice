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
        [HttpGet]
        [Route("[action]")]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(LoginViewModel model)
        {
            return View();
        }
    }
}
