using Company.Data.Entities;
using Company.Service.Helper;
using Company.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
        [HttpPost]
        public async Task<IActionResult>SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    if(await _userManager.CheckPasswordAsync(user, model.Password)) 
                    {
                        // use sign in manager
                        // IsPeresist => the cookie and data of user still exist or not
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
                        if (result.Succeeded)
                            return RedirectToAction("Index","Home");                       
                    }
                }
                ModelState.AddModelError("", "Incorrect Email or Password");
                return View(model);
            }
            return View(model);
        }
        #endregion

        #region SignOut
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        #region ForgetPassword
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel model)
        {
            if (ModelState.IsValid)
            {   
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    // make token to know which user will change password
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    // gether link
                    var url = Url.Action("ResetPassword", "Account", new
                    {
                        Email = model.Email,
                        Token = token
                    },Request.Scheme// define my HTTP 
                    );

                    var email = new Email
                    {
                        Body = url,
                        Subject = "Reset Password",
                        To= model.Email
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
            }
            return View();
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }

        public IActionResult ResetPassword(string Email , string Token)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null) 
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token,model.Password);
                    if (result.Succeeded) 
                    { 
                        return RedirectToAction(nameof(SignIn));
                    }
                    foreach (var error in result.Errors)                    
                        ModelState.AddModelError("", error.Description);
                    
                }

            }
            return View(model);
        }
        #endregion


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
