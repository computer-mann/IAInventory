using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Accounts.Models;
using WebApp.Areas.Accounts.Models.DbContexts;
using WebApp.Areas.Accounts.ViewModels;
using WebApp.Models.DbContexts;

namespace WebApp.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class AdminController : Controller
    {
        private readonly StoreDbContext _context;
        private UserManager<Staff> _userManager;

        public AdminController(UserManager<Staff> userManager)
        {
            _userManager = userManager;
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
                    UserName = user.UserName,
                    FullName = user.FullName,
                    EmailConfirmed = true
                };
                if ((await _userManager.FindByNameAsync(user.UserName)) != null)
                {
                    return Problem("User exists");
                }
                var role = new IdentityRole<int>("Attendant");
                var createUser = await _userManager.CreateAsync(newUser, user.Password);
                if (createUser.Succeeded)
                {
                    await  _userManager.AddToRoleAsync(newUser, "Attendant");
                    await _userManager.AddClaimAsync(newUser, new Claim("Role", "Attendant"));
                    ViewBag.Success = "Created Successfully";
                    return View();
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
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null) return View(model);
            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (result.Succeeded)
            {
                ViewBag.Success = "Changed Successfully";
                return View();
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

        [Route("/admin/index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }



    }
}
