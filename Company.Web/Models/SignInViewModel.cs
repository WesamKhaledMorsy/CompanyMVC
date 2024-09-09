using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class SignInViewModel
    {
        [EmailAddress(ErrorMessage = "Invalid Format For Email")]
        public string Email { get; set; }
        [RegularExpression(@"^(?=(.*[A-Z]){1,})(?=(.*[a-z]){1,})(?=(.*\d){1,})(?=(.*\W){1,})(?!.*(.)\1{2,}).{6,}$",
             ErrorMessage = "Password must be at least 6 characters long, with at least one uppercase letter, one lowercase letter, one digit, and one special character, and must contain at least two unique characters.")]
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
