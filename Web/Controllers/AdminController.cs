using System.Threading.Tasks;
using Application.Common.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IIdentityService _identityService;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IIdentityService identityService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _identityService = identityService;
        }

        public IActionResult Index()
        {
            return View(nameof(Users));
        }

        public async Task<IActionResult> Users()
        {
            return View(new AdminViewModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Users(AdminViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
            if (user != null)
            {
                ModelState.AddModelError(string.Empty,"User Already exists");
            }

            var (result,_) = await _identityService.CreateUserAsync(model.Name, model.Email, model.Password).ConfigureAwait(false);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("",error);
            }

            return ModelState.IsValid ? Index() : View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user != null && await _roleManager.RoleExistsAsync(roleName).ConfigureAwait(false))
            {
                await _userManager.AddToRoleAsync(user, roleName).ConfigureAwait(false);
            }

            return Index();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user != null && await _roleManager.RoleExistsAsync(roleName).ConfigureAwait(false) &&
                await _userManager.IsInRoleAsync(user, roleName).ConfigureAwait(false))
            {
                await _userManager.RemoveFromRoleAsync(user, roleName).ConfigureAwait(false);
            }

            return Index();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(AdminViewModel model)
        {
            if (!ModelState.IsValid) return Index();

            var user = await _userManager.FindByIdAsync(model.UserId).ConfigureAwait(false);
            user.UserName = model.Name;
            user.Email = model.Email;

            await _userManager.UpdateAsync(user).ConfigureAwait(false);


            return Index();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _identityService.DeleteUserAsync(userId).ConfigureAwait(false);

            return Index();
        }
    }
}