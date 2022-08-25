using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.Areas.StaffAccount.Models;

namespace WebApi.Areas.Models.StaffAccount.DbContexts
{
    public class StaffDbContext:IdentityDbContext<Staff,IdentityRole<int>,int>
    {
        public StaffDbContext(DbContextOptions<StaffDbContext> options):base(options)
        {

        }
    }
}
