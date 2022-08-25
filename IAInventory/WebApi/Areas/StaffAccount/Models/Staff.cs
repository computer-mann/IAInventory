using Microsoft.AspNetCore.Identity;

namespace WebApi.Areas.StaffAccount.Models
{
    public class Staff : IdentityUser<int>
    {
        public override int Id { get; set; }
        public string FullName { get; set; }
    }
}
