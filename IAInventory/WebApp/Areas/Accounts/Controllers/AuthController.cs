
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
                    return Redirect("/admin");
                }
                else
                {
                    return Redirect("/products");
                }
            }
            return View(model);
        }

        [Route("/createuser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var newUser = new Staff()
                {
                    Email = user.Email,
                    UserName = user.Username,
                    FullName = user.FullName,
                    EmailConfirmed = true
                };
                if ((await _userManager.FindByNameAsync(user.Username)) != null)
                {
                    return Problem("User exists");
                }
                var createUser = await _userManager.CreateAsync(newUser, user.Password);
                if (createUser.Succeeded)
                {
                    return View(newUser);
                }
                
            }
            return Problem("Please provide alll parameters.");
        }

        [Route("/createuser")]
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [Route("/resetuserpassword")]
        [HttpPost]
        public async Task<IActionResult> ResetUserPassword(ResetUserPasswordViewModel model)
        {
           if(!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByNameAsync(model.Username);
            if(user ==  null) return View(model);
            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (result.Succeeded)
            {
                return View(new {message="Success" });
            }
            else
            {
                return Problem("problem");
            }
        }

        [Route("/resetuserpassword")]
        [HttpGet]
        public async Task<IActionResult> ResetUserPassword()
        {
            return View();
        }



    }
}
