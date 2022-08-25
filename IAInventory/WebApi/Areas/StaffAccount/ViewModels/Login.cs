using System.ComponentModel.DataAnnotations;

namespace WebApi.Areas.StaffAccount.ViewModels
{
    public class Login
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
