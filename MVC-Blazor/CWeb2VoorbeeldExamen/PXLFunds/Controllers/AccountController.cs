using Microsoft.AspNetCore.Mvc;
using PXLFunds.Models;
using PXLFunds.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using PXLFunds.ViewModels;
using SignInResult = Microsoft.AspNetCore.Mvc.SignInResult;

namespace PXLFunds.Controllers
{
    public class AccountController : Controller
    {
        IUserLoginRepository _loginRepo;
        public AccountController(IUserLoginRepository loginRepo)
        {
            _loginRepo = loginRepo;
        }
        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await _loginRepo.LoginAsync(null, login.Email, login.Password);
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
        #endregion

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // This is a task because we need to await the creation of the new user before we do anything else
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterModel registerData)
        {
            if (ModelState.IsValid)
            {
                var result = await _loginRepo.RegisterAsync(registerData);
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


        public IActionResult Logout()
        {
            _loginRepo.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
        #region External login

        public IActionResult IdentityServerLogin()
        {
            string redirectUrl = Url.Action("ExternalLoginResponse", "Account");
            string authScheme = OpenIdConnectDefaults.AuthenticationScheme;
            var properties = _loginRepo.GetExAuthProperties(authScheme, redirectUrl);
            return new ChallengeResult(authScheme, properties);
        }

        // The response for the url.action
        public async Task<IActionResult> ExternalLoginResponse()
        {
            ExternalLoginInfo externalLoginInfo = await _loginRepo.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null) return RedirectToAction(nameof(Login));
            var userInfo = GetUserInfoViewModel(externalLoginInfo);

            var result = await _loginRepo.ExternalLoginAsync(externalLoginInfo, userInfo);
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

            if (externalLoginInfo.Principal.FindFirst(ClaimTypes.Name) != null)
            {
                string fullName = externalLoginInfo.Principal.FindFirst(ClaimTypes.Name).Value;
                userName = fullName.Replace(" ", "");
            }
            else
            {
                string fullName = externalUser.FindFirst("name").Value;
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
    }
}
