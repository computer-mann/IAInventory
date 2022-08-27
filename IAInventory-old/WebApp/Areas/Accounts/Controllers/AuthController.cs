
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebApp.Areas.Accounts.Models;
using WebApp.Areas.Accounts.ViewModels;
using WebApp.Models.DbContexts;

namespace WebApp.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class AuthController : Controller
    {
        private UserManager<Staff> _userManager;
        private SignInManager<Staff> _signInManager;

        public AuthController(UserManager<Staff> userManager, SignInManager<Staff> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            
        }

        [Route("/login")]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid) return View(model);
            var user=await _userManager.FindByNameAsync(model.Username);
            if(user != null)
            {
                await _signInManager.SignInAsync(user, true);
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return Redirect("/admin/index");
                }
                else
                {
                    return Redirect("/products/index");
                }
            }
            return View(model);
        }
        [Route("/logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return Redirect("/login");
        }

        [Route("/access-denied")]
       public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
