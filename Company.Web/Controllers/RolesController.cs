using Company.Data.Entities;
using Company.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public RolesController(RoleManager<ApplicationRole> roleManager, ILogger<UserController> logger, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _logger=logger;
            _userManager=userManager;
        }
        public async Task<IActionResult>Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public IActionResult Create() 
        {
            return View(new ApplicationRole());
        }
        [HttpPost]
        public async Task<IActionResult> Create( ApplicationRole role) 
        {
            if (ModelState.IsValid) 
            { 
                var reult = await _roleManager.CreateAsync(role);
                if (reult.Succeeded)                
                    return RedirectToAction("Index");
                foreach (var error in reult.Errors)
                {
                    _logger.LogError(error.Description);
                    ModelState.AddModelError("", error.Description);
                }
                return View(role);
            }
            return View(role);
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            return View(viewName, role);
        }

        public async Task<IActionResult> Update(string id)
        {
            // it will make the same process of Details 
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationRole applicationRole)
        {
            if (id != applicationRole.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = applicationRole.Name;
                    role.CreateAt = applicationRole.CreateAt;

                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    foreach (var error in result.Errors)
                    {
                        _logger.LogError(error.Description);
                        ModelState.AddModelError("", error.Description);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            return View(applicationRole);

        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null)
                    return NotFound();
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                foreach (var error in result.Errors)
                {
                    _logger.LogError(error.Description);
                    ModelState.AddModelError("", error.Description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToAction("Index");
        }


        //public IActionResult AddOrRemoveUsers() 
        //{
        //    return View();
        //}
        //[HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
          
            if (role is null)
                return NotFound();
        
            var users=await _userManager.Users.ToListAsync();
            var usersInRole = new List<UserInRoleViewModel>();
            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if(await _userManager.IsInRoleAsync(user, role.Name))                
                    userInRole.IsSelected = true;
                else 
                    userInRole.IsSelected = false;
               usersInRole.Add(userInRole);
            }
            ViewBag.rolename = role.Name;
            ViewBag.RoleId = roleId;
            return View(usersInRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId,List<UserInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser =  await _userManager.FindByIdAsync(user.UserId);
                    if(appUser is not null)
                    {
                        if(user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name))                        
                            await _userManager.AddToRoleAsync(appUser, role.Name);  
                        else if(!user.IsSelected && await _userManager.IsInRoleAsync(appUser,role.Name) )
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                    }
                }
                return RedirectToAction("Update", new {id = roleId});
            }
            return View(users);
        }
    }
}
