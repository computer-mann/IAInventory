using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.Accounts.ViewModels
{
    public class CreateUserViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
