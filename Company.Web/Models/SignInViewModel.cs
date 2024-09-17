using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Format For Email")]
        public string Email { get; set; }       
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
