using System.Threading.Tasks;
using HrApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HrApp.Services
{
    // dont forget to make interfaces public at all times
    public interface IdentityRepositoryInterface
    {
        Task<IdentityRepositoryResult> LoginAsync(string username, string email, string password);
        Task<IdentityRepositoryResult> RegisterAsync(RegisterViewModel registerData);
        Task LogoutAsync();

        Task<IdentityRepositoryResult> ExternalLoginAsync(ExternalLoginInfo externalLoginInfo, UserInfoViewModel userInfoViewModel);
        AuthenticationProperties GetExAuthProperties(string provider, string redirectUrl);
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
    }
}
