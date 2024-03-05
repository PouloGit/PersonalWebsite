using System.ComponentModel.DataAnnotations;

namespace ASP_Projects.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter a email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
