using System.ComponentModel.DataAnnotations;

namespace WebApi.Areas.StaffAccount.ViewModels
{
    public class ResetUserPassword
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
