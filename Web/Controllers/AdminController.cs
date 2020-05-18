using System.Threading.Tasks;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View(nameof(Users));
        }

        public async Task<IActionResult> Users()
        {
            return View(await _userManager.Users.ToListAsync().ConfigureAwait(false));
        }

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
    }
}