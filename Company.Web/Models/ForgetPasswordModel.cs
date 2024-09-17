using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class ForgetPasswordModel
    {
        [Required(ErrorMessage ="Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Format For Email")]
        public string Email { get; set; }
    }
}
