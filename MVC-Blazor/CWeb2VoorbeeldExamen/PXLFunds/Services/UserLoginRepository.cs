using PXLFunds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using PXLFunds.Data;
using PXLFunds.ViewModels;

namespace PXLFunds.Services
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public UserLoginRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signinManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signinManager = signinManager;
            _roleManager = roleManager;
        }

        #region Login with username OR email
        private async Task<UserLoginRepositoryResult> LoginAsync(IdentityUser user, string password)
        {
            var result = new UserLoginRepositoryResult();
            var identityResult = await _signinManager.PasswordSignInAsync(user, password, false, false);

            if (identityResult.Succeeded)
            {
                result.Succeeded = true;
            }
            else
            {
                result.AddError("Could not log in");
            }
            return result;
        }

        public async Task<UserLoginRepositoryResult> LoginAsync(string username, string email, string password)
        {
            var result = await GetIdentityUserAsync(username, email, password);
            if (result.Succeeded)
            {
                result = await LoginAsync(result.IdentityUser, password);
            }
            else
            {
                result.AddError("Wrong username or password");
            }

            return result;
        }
        #endregion



        #region regular register with email and username
        public async Task<UserLoginRepositoryResult> RegisterAsync(RegisterModel registerData)
        {
            var result = new UserLoginRepositoryResult();
            var identityUser = new IdentityUser
            {
                UserName = registerData.UserName,
                Email = registerData.Email
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerData.Password);
            if (identityResult.Succeeded)
            {
                var roleExists = await _roleManager.RoleExistsAsync(Settings.ProgramInfo.Roles.ClientRole);
                if (roleExists)
                {
                    await _userManager.AddToRoleAsync(identityUser, Settings.ProgramInfo.Roles.ClientRole);
                }
                else
                {
                    await _roleManager.CreateAsync(new IdentityRole(Settings.ProgramInfo.Roles.ClientRole));
                    await _userManager.AddToRoleAsync(identityUser, Settings.ProgramInfo.Roles.ClientRole);
                }

                result.Succeeded = true;
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    result.AddError(error.Description);
                }
            }
            return result;
        }
        #endregion


        #region Helper functions such as GetIdentityUser and Logout
        private async Task<UserLoginRepositoryResult> GetIdentityUserAsync(string userName, string email,
            string password)
        {
            var result = new UserLoginRepositoryResult();
            IdentityUser user = null;
            if (userName != null)
            {
                user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    result.AddError("Could not find a user with this username");
                }
            }
            else
            {
                if (email != null)
                {
                    user = await _userManager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        result.AddError("Could not find a user with this email");
                    }
                }
            }

            if (user != null)
            {
                result.IdentityUser = user;
                var identityResult = await _signinManager.PasswordSignInAsync(user, password, false, false);
                if (identityResult.Succeeded)
                {
                    result.Succeeded = true;

                }
            }

            return result;
        }

        public async Task LogoutAsync()
        {
            await _signinManager.SignOutAsync();
        }

        #endregion

        #region External identityserver login

        public async Task<UserLoginRepositoryResult> ExternalLoginAsync(ExternalLoginInfo externalLoginInfo, UserInfoViewModel userInfoViewModel)
        {
            var result = new UserLoginRepositoryResult();
            result.SignInResult = await _signinManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider,
                externalLoginInfo.ProviderKey, false);
            if (!result.SignInResult.Succeeded)
            {
                IdentityUser user = new IdentityUser(userInfoViewModel.UserName)
                {
                    Email = userInfoViewModel.Email
                };

                result.IdentityUserCreationResult = await _userManager.CreateAsync(user);
                if (result.IdentityUserCreationResult.Succeeded)
                {
                    // add your needed role here
                    var roleExists = await _roleManager.RoleExistsAsync(Settings.ProgramInfo.Roles.ClientRole);
                    if (roleExists)
                    {
                        await _userManager.AddToRoleAsync(user, Settings.ProgramInfo.Roles.ClientRole);
                    }
                    else
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Settings.ProgramInfo.Roles.ClientRole));
                        await _userManager.AddToRoleAsync(user, Settings.ProgramInfo.Roles.ClientRole);
                    }

                    // if you need to connect to the context here and add the user to a seperate client class do it here
                    //_context.Clients.AddRange(
                    //    new Client
                    //    {
                    //        IdentityUserId = user.Id,
                    //        FirstName = userInfoViewModel.FirstName,
                    //        Name = userInfoViewModel.LastName
                    //    });
                    //await _context.SaveChangesAsync();

                    result.IdentityExternalLoginResult = await _userManager.AddLoginAsync(user, externalLoginInfo);
                    if (result.IdentityExternalLoginResult.Succeeded)
                    {
                        await _signinManager.SignInAsync(user, false);
                    }
                }

            }
            return result;
        }

        public AuthenticationProperties GetExAuthProperties(string provider, string redirectUrl)
        {
            return _signinManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await _signinManager.GetExternalLoginInfoAsync();
        }

        #endregion
    }

}


