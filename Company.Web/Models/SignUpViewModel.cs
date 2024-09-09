using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="First Name is Required")]
        public string FirstName {  get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage ="Invalid Format For Email")]
        public string Email { get; set; }
        [RegularExpression(@"^(?=(.*[A-Z]){1,})(?=(.*[a-z]){1,})(?=(.*\d){1,})(?=(.*\W){1,})(?!.*(.)\1{2,}).{6,}$",
             ErrorMessage = "Password must be at least 6 characters long, with at least one uppercase letter, one lowercase letter, one digit, and one special character, and must contain at least two unique characters.")]
        [Required(ErrorMessage ="Password is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword is Required")]
        [Compare(nameof(Password),ErrorMessage ="Confirm password does not match Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="Required To Agree")]
        public bool IsAgree { get; set; }

    }
}
