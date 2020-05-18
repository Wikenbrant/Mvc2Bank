using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "Cashier" };
            if (!roleManager.Roles.Any(r => roles.Contains(r.Name)))
            {
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole(role)).ConfigureAwait(false);
                }
            }
        }
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "Admin", Email = "admin@admin.se" };
            var stefanAdmin = new ApplicationUser { UserName = "StefanAdmin", Email = "stefan.holmberg@systementor.se" };
            var stefanCashier = new ApplicationUser { UserName = "StefanCashier", Email = "stefan.holmberg@nackademin.se" };

            if (!userManager.Users.Any(u => u.UserName == defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Password123!").ConfigureAwait(false);
                await userManager.AddToRoleAsync(defaultUser, "Admin").ConfigureAwait(false);
            }
            if (!userManager.Users.Any(u => u.UserName == stefanAdmin.UserName))
            {
                await userManager.CreateAsync(stefanAdmin, "Hejsan123#").ConfigureAwait(false);
                await userManager.AddToRoleAsync(stefanAdmin, "Admin").ConfigureAwait(false);
            }
            if (!userManager.Users.Any(u => u.UserName == stefanCashier.UserName))
            {
                await userManager.CreateAsync(stefanCashier, "Hejsan123#").ConfigureAwait(false);
                await userManager.AddToRoleAsync(stefanCashier, "Cashier").ConfigureAwait(false);
            }
        }
    }
}