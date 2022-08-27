using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.Accounts.ViewModels
{
    public class ResetUserPasswordViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
