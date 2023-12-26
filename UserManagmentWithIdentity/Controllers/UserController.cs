using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using UserManagmentWithIdentity.Migrations;
using UserManagmentWithIdentity.Models;
using UserManagmentWithIdentity.ViewModels;

namespace UserManagmentWithIdentity.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager )
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Select(user=> new UserViewModel
            {
                Id = user.Id,
                FullName=user.FullName,
                Email=user.Email,
                UserName= user.UserName,
                Roles = _userManager.GetRolesAsync(user).Result

            }).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var ViewModel = new ProfileFormViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email

            };
            return View(ViewModel);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Edit(ProfileFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
                return NotFound();

            var UserWithSameUserName = await _userManager.FindByNameAsync(model.UserName);
            if (UserWithSameUserName != null && UserWithSameUserName.Id != model.Id)
            {
                ModelState.AddModelError("UserName", "UserName already Exist");
                return View(model);
            }
            var UserWithSameEmail= await _userManager.FindByEmailAsync(model.Email);
            if (UserWithSameEmail != null && UserWithSameEmail.Id != model.Id)
            {
                ModelState.AddModelError("Email", "Email already Exist");
                return View(model);
            }
            user.FullName = model.FullName;
            user.Email = model.Email;
            user.UserName = model.UserName;
            await _userManager.UpdateAsync(user);


            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Add()
        {   
            var roles = await _roleManager.Roles.Select(r => new RoleViewModel {RoleId=r.Id ,RoleName = r.Name}).ToListAsync();
            var ViewModel = new AddUserViewModel
            {
              Roles = roles
            };
            return View(ViewModel);
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (!model.Roles.Any(r => r.IsSelected))
            {
                ModelState.AddModelError("Roles", "Please select any roles ");
                return View(model);
            }
            if (await _userManager.FindByEmailAsync(model.Email)!=null)
            {
                ModelState.AddModelError("Email", "Email Already Exist ");
                return View(model);
            }
            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("Email", "UserName Already Exist ");
                return View(model);
            }
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    //ModelState.AddModelError("Roles", error.Description);
                }
                return View(model);

            }
            await _userManager.AddToRolesAsync(user, model.Roles.Where(u => u.IsSelected).Select(x => x.RoleName));
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var roles = await _roleManager.Roles.ToListAsync();
            var ViewModel = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(role => new RoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()

            };
            return View(ViewModel);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound();
            var userRole = await _userManager.GetRolesAsync(user);
            foreach (var item in model.Roles)
            {
                if(userRole.Any(r=>r==item.RoleName)&&!item.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user,item.RoleName);
                if (!userRole.Any(r => r == item.RoleName) && item.IsSelected)
                    await _userManager.AddToRoleAsync(user, item.RoleName);

            }

            return RedirectToAction(nameof(Index));
        }
    }
}

