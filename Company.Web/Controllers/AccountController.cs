using Company.Data.Entities;
using Company.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        #region SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel input)
        {
            if (ModelState.IsValid) 
            {
                var user = new ApplicationUser
                {
                    UserName = input.Email.Split("@")[0],
                    Email = input.Email,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    IsActive = true
                };
                 
                // create thie user
                var userResult =await _userManager.CreateAsync(user,input.Password);
                if (userResult.Succeeded)                
                    return RedirectToAction("SignIn");
                foreach (var error in userResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View();
        }
        #endregion

        #region Sign In
        public IActionResult SignIn()
        {
            return View();
        }

        #endregion

    }
}
