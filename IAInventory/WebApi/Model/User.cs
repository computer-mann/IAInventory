using Microsoft.AspNetCore.Identity;

namespace WebApi.Model
{
    public class User:IdentityUser
    {
        public int Id { get; set; }
    }
}
