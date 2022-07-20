using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using RetroGamingAPI.Models;

namespace RetroGamingAPI.Services
{
    public interface IdentityRepositoryInterface
    {
        Task<IdentityRepositoryResult> LoginAsync(string username, string email, string password);
        Task<IdentityRepositoryResult> RegisterAsync(RegistrationModel registerData);
        Task LogoutAsync();

        Task<IdentityRepositoryResult> ExternalLoginAsync(ExternalLoginInfo externalLoginInfo, UserInfoViewModel userInfoViewModel);
        AuthenticationProperties GetExAuthProperties(string provider, string redirectUrl);
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
    }
}
