using System.ComponentModel.DataAnnotations;

namespace ASP_Projects.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter a email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} and most {1} characters long", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; } = "";
    }
}
