using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.Accounts.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
