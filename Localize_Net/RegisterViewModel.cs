using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Localize_Net
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "The email field is required")]
        [EmailAddress(ErrorMessage = "the email field is not a valid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="the email field is required")]
        [StringLength(8,ErrorMessage ="The {0} must be atleast {2} characters long.",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        [Compare("Password",ErrorMessage ="The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set;}
    }
}