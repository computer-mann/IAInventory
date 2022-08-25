using System.ComponentModel.DataAnnotations;

namespace WebApi.Areas.StaffAccount.ViewModels
{
    public class CreateUser
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
