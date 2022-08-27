using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebApp.Areas.Accounts.Models;

namespace WebApp.Configuration
{
    public class DbInitializer
    {

        private RoleManager<IdentityRole<int>> _roleMgr;
        private UserManager<Staff> _userMgr;

        public DbInitializer(RoleManager<IdentityRole<int>> roleManager, UserManager<Staff> userManager)
        {
            _roleMgr = roleManager;
            _userMgr = userManager;
        }


        public async Task popolateDb()
        {

            var user = await _userMgr.FindByNameAsync("yaagyekye17");

            if (user == null)
            {
                if (!(await _roleMgr.RoleExistsAsync("Admin")))
                {
                    var role = new IdentityRole<int>("Admin");
                    var role2 = new IdentityRole<int>("Attendant");

                    await _roleMgr.CreateAsync(role);
                    await _roleMgr.CreateAsync(role2);
                }

                user = new Staff()
                {
                    UserName = "yaagyekye17",
                    EmailConfirmed = true,
                    FullName = "yaa gyekye17",
                    Email = "yaagyekye17@gmail.com"
                };

                var userResult = await _userMgr.CreateAsync(user, "1234");
                var roleResult = await _userMgr.AddToRoleAsync(user, "Admin");
                var claimResult = await _userMgr.AddClaimAsync(user, new Claim("Role", "Admin"));

                if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }

            }


        }

    }
}
