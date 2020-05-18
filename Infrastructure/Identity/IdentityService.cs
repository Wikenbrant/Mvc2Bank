using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityService(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> GetUserNameAsync(string userId) =>
            (await _userManager.Users.FirstAsync(u => u.Id == userId).ConfigureAwait(false)).UserName;


        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName,string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
            };

            var result = await _userManager.CreateAsync(user, password).ConfigureAwait(false);
            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user).ConfigureAwait(false);
            }

            return Result.Success();
        }

        public async Task<Result> SignInUserAsync(string email,string password, bool rememberMe )
        {
           

            var user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);

            if (user == null)
            {
                return Result.Failure(new []{"Invalid username"});
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: false)
                .ConfigureAwait(false);


            
            return result.ToApplicationResult();
        }

        public Task SignOutUserAsync() => _signInManager.SignOutAsync();

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user).ConfigureAwait(false);

            return result.ToApplicationResult();
        }
    }
}
