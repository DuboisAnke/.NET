using HrApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HrApp.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HrApp.Controllers
{
    public class AccountController : Controller
    {
        // dependency injections
        private IdentityRepositoryInterface _repo;

        public AccountController(IdentityRepositoryInterface repo)
        {
            _repo = repo;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult LoginWithUserName()
        {
            return View();
        }


        public IActionResult LoginWithEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginWithUserNameAsync(LoginUsernameViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await _repo.LoginAsync(login.UserName, null, login.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
            }
            return View(login);
        }

        [HttpPost]
        public async Task<IActionResult> LoginWithEmailAsync(LoginEmailViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await _repo.LoginAsync(null, login.Email, login.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
            }
            return View(login);
        }

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // This is a task because we need to await the creation of the new user before we do anything else
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerData)
        {
            if (ModelState.IsValid)
            {
                var result = await _repo.RegisterAsync(registerData);
                if (result.Succeeded)
                {
                    return View("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }

            }
            return View(registerData);
        }
        #endregion

        #region External login

        public IActionResult IdentityServerLogin()
        {
            string redirectUrl = Url.Action("ExternalLoginResponse", "Account");
            string authScheme = OpenIdConnectDefaults.AuthenticationScheme;
            var properties = _repo.GetExAuthProperties(authScheme, redirectUrl);
            return new ChallengeResult(authScheme, properties);
        }

        // The response for the url.action
        public async Task<IActionResult> ExternalLoginResponse()
        {
            ExternalLoginInfo externalLoginInfo = await _repo.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null) return RedirectToAction(nameof(Login));
            var userInfo = GetUserInfoViewModel(externalLoginInfo);

            var result = await _repo.ExternalLoginAsync(externalLoginInfo, userInfo);
            if (!result.SignInResult.Succeeded)
            {
                if (!result.IdentityUserCreationResult.Succeeded)
                {
                    return View("Login");
                }
                else
                {
                    if (!result.IdentityExternalLoginResult.Succeeded)
                    {
                        return View("Login");
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        // Get all the users infomation
        UserInfoViewModel GetUserInfoViewModel(ExternalLoginInfo externalLoginInfo)
        {
            var externalUser = externalLoginInfo.Principal;
            string userName;
            string firstName;
            string lastName;

            if (externalLoginInfo.Principal.FindFirst(ClaimTypes.Name) != null)
            {
                //you can delete this if you dont need to set first and last name, this part is also for other external logins, like google, fb
                string fullName = externalLoginInfo.Principal.FindFirst(ClaimTypes.Name).Value;
                //string[] names = fullName.Split('.');
                //firstName = names[0];
                //lastName = names[1];
                userName = fullName.Replace(" ", "");
            }
            else
            {
                // this is the part you'd use to get the name.Other claimtypes are in there as well
                // see email down here, just repeat this for every property you need to fill in 
                string fullName = externalUser.FindFirst("name").Value;


                //string[] names = fullName.Split('.');
                //firstName = names[0];
                //lastName = names[1];
                userName = fullName.Replace(" ", "");
            }

            string email;
            if (externalLoginInfo.Principal.FindFirst(ClaimTypes.Email) != null)
            {
                email = externalLoginInfo.Principal.FindFirst(ClaimTypes.Email).Value;
            }
            else
            {
                email = externalLoginInfo.Principal.FindFirst("email").Value;
            }

            UserInfoViewModel userInfoViewModel = new UserInfoViewModel
            {
                UserName = userName,
                Email = email,
            };
            return userInfoViewModel;
        }

        #endregion
        public async Task<IActionResult> Logout()
        {
            await _repo.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
