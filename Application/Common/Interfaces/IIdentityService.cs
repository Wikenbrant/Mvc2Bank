using System.Threading.Tasks;
using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string email, string password);

        Task<Result> DeleteUserAsync(string userId);

        Task<Result> SignInUserAsync(string email, string password, bool rememberMe);
        Task SignOutUserAsync();
    }
}