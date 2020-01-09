using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcFrameworkCml;
using MvcFrameworkCml.Infrastructure;
using MvcFrameworkCml.Infrastructure.Managers;
using MvcFrameworkCml.Transfer;
using MvcFrameworkCml.ViewModels;
using MvcFrameworkWeb.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcFrameworkWeb.Controllers
{
    public class LoginController : CustomBaseController
    {
        private readonly IEndUserManager _userLogicManager_;

        public LoginController(
            IEndUserManager userLogicManager,
            ControllerHelper controllerHelper,
            IAppSettings appSettings,
            ILogger logger
            ) : base(controllerHelper, appSettings, logger)
        {
            _userLogicManager_ = userLogicManager;
        }

        [AllowAnonymous]
        public async Task<ActionResult<Content<EndUser>>> LogIn([FromBody]UserLoginData userLogin)
        {
            var content = await _userLogicManager_.AuthenticateUserAsync(userLogin.Email, userLogin.Password);
            if (!content.HasError)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, content.Data.Name),
                    new Claim(ClaimTypesExt.LastName, content.Data.LastName),
                    new Claim(ClaimTypes.Role, content.Data.Role),
                    new Claim(ClaimTypesExt.Id, content.Data.Id.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = false,

                    //todo move to config
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(_appSettings_.CookieExpirePeriodInMins),

                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }

            return content;
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public async Task LogOut()
        {
            await HttpContext.SignOutAsync();
        }

        public async Task CreateAdmin()
        {
            await CreateAdminUser();
        }

        public IActionResult Index()
        {
            var res = GetViewModelOrRedirect(HttpContext);
            return View();
        }

        private async Task CreateAdminUser()
        {
            await _userLogicManager_.AddEntityAsync(
                new EndUser()
                {
                    Email = "admin@mail.com",
                    EmailConfirmed = true,
                    Name = "Mr.",
                    LastName = "Admin",
                    Password = "admin",
                    Role = "admin"
                });
        }
    }
}
