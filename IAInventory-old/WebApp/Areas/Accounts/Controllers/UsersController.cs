using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Accounts.Models;
using WebApp.Areas.Accounts.Models.DbContexts;
using WebApp.Areas.Accounts.ViewModels;

namespace WebApp.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    [Authorize(Roles = "Admin")]
    [Route("{area}/{controller}/{action}/{id?}")]
    public class UsersController : Controller
    {
        private readonly StaffDbContext _context;
        private readonly UserManager<Staff> userManager;

        public UsersController(StaffDbContext context, UserManager<Staff> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Accounts/Users
       // [Route("/users")]
        public async Task<IActionResult> Index()
        {
            var listOfUsers = new List<CreateUserViewModel>();
            var all = userManager.Users;
            foreach (var user in all)
            {
                listOfUsers.Add(new CreateUserViewModel()
                {
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Email = user.Email,
                    //Role =(await userManager.IsInRoleAsync(user,"Admin")) ? "Admin" : "Attendant"
                });
            }
              return View(listOfUsers);
        }

        // GET: Accounts/Users/Details/5
        
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var staff = await _context.Users
                .FirstOrDefaultAsync(m => m.UserName == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Accounts/Users/Create
        //[Route("/users/create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Route("/users/create")]
        public async Task<IActionResult> Create([Bind("FullName,UserName,Email,EmailConfirmed,PasswordHash")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                var staff1 = new Staff()
                {
                    UserName = staff.UserName,
                    FullName = staff.FullName,
                    Email = staff.Email,
                };
                await userManager.CreateAsync(staff1, staff.PasswordHash);
                await userManager.AddToRoleAsync(staff1, "Attendant");
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET: Accounts/Users/Edit/5
        //[Route("/users/edit")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var staff = await _context.Users.Where(x=>x.UserName == id).FirstAsync();
            if (staff == null)
            {
                return NotFound();
            }
            staff.PasswordHash = "";
            return View(staff);
        }

        // POST: Accounts/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
      //  [Route("/users/edit")]
        public async Task<IActionResult> Edit(int id, [Bind("UserName,PasswordHash")] Staff staff)
        {
            if (id != staff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var staff1 = await userManager.FindByNameAsync(staff.UserName);
                    await userManager.RemovePasswordAsync(staff1);
                    await userManager.AddPasswordAsync(staff1, staff.PasswordHash);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET: Accounts/Users/Delete/5
       // [Route("/users/delete")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var staff = await _context.Users
                .FirstOrDefaultAsync(m => m.UserName == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Accounts/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'StaffDbContext.Users'  is null.");
            }
            var staff = await _context.Users.Where(x=>x.UserName == id).FirstAsync();
            if (staff != null)
            {
                _context.Users.Remove(staff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
          return _context.Users.Any(e => e.Id == id);
        }
    }
}
