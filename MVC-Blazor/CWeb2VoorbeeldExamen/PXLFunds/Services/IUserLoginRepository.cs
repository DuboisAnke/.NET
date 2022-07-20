using PXLFunds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using PXLFunds.ViewModels;

namespace PXLFunds.Services
{
    public interface IUserLoginRepository
    {
        Task<UserLoginRepositoryResult> LoginAsync(string username, string email, string password);
        Task<UserLoginRepositoryResult> RegisterAsync(RegisterModel registerData);
        Task LogoutAsync();

        Task<UserLoginRepositoryResult> ExternalLoginAsync(ExternalLoginInfo externalLoginInfo, UserInfoViewModel userInfoViewModel);
        AuthenticationProperties GetExAuthProperties(string provider, string redirectUrl);
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
    }
}
