using System.Threading.Tasks;
using HrApp.Data;
using HrApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HrApp.Services
{
    public class IdentityRepository : IdentityRepositoryInterface
    {
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public IdentityRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        #region Login with username OR email
        private async Task<IdentityRepositoryResult> LoginAsync(IdentityUser user, string password)
        {
            var result = new IdentityRepositoryResult();
            var identityResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

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

        public async Task<IdentityRepositoryResult> LoginAsync(string username, string email, string password)
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
        public async Task<IdentityRepositoryResult> RegisterAsync(RegisterViewModel registerData)
        {
            var result = new IdentityRepositoryResult();
            var identityUser = new IdentityUser
            {
                UserName = registerData.UserName,
                Email = registerData.Email
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerData.Password);
            if (identityResult.Succeeded)
            {
                var roleExists = await _roleManager.RoleExistsAsync(Settings.Roles.UserRole);
                if (roleExists)
                {
                    await _userManager.AddToRoleAsync(identityUser, Settings.Roles.UserRole);
                }
                else
                {
                    await _roleManager.CreateAsync(new IdentityRole(Settings.Roles.UserRole));
                    await _userManager.AddToRoleAsync(identityUser, Settings.Roles.UserRole);
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
        private async Task<IdentityRepositoryResult> GetIdentityUserAsync(string userName, string email,
            string password)
        {
            var result = new IdentityRepositoryResult();
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
                var identityResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (identityResult.Succeeded)
                {
                    result.Succeeded = true;
                    
                }
            }

            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        #endregion

        #region External identityserver login

        public async Task<IdentityRepositoryResult> ExternalLoginAsync(ExternalLoginInfo externalLoginInfo, UserInfoViewModel userInfoViewModel)
        {
            var result = new IdentityRepositoryResult();
            result.SignInResult = await _signInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider,
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
                    var roleExists = await _roleManager.RoleExistsAsync(Settings.Roles.UserRole);
                    if (roleExists)
                    {
                        await _userManager.AddToRoleAsync(user, Settings.Roles.UserRole);
                    }
                    else
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Settings.Roles.UserRole));
                        await _userManager.AddToRoleAsync(user, Settings.Roles.UserRole);
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
                        await _signInManager.SignInAsync(user, false);
                    }
                }

            }
            return result;
        }

        public AuthenticationProperties GetExAuthProperties(string provider, string redirectUrl)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await _signInManager.GetExternalLoginInfoAsync();
        }

#endregion
    }
}
