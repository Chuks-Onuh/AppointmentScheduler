using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduler.ViewModels
{
    public class RegisterVm
    {
        [Required]
        public string FirstName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]

        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The password and confirm password do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]

        public string ConfirmPassword { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
