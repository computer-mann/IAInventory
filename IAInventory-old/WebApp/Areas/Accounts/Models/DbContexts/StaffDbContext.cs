using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Accounts.Models.DbContexts
{
    public class StaffDbContext : IdentityDbContext<Staff, IdentityRole<int>, int>
    {
        public StaffDbContext(DbContextOptions<StaffDbContext> options) : base(options)
        {

        }
    }
}
