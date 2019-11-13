using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcFrameworkBll;
using MvcFrameworkCml;
using MvcFrameworkWeb.Models;
using MvcFrameworkWeb.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcFrameworkWeb.Controllers
{
    public class LoginController : CustomBaseController
    {
        private readonly UserLogicManager _userLogicManager_;
        public LoginController(UserLogicManager userLogicManager, ControllerHelper controllerHelper) : base(controllerHelper)
        {
            _userLogicManager_ = userLogicManager;
        }

        [AllowAnonymous]
        public async Task<ActionResult<EndUser>> LogIn([FromBody]UserLoginData userLogin)
        {
            //CreateAdminUser();
            var user = _userLogicManager_.Authenticate(userLogin.Email, userLogin.Password);
            if (user != null)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypesExt.Id, user.Id.ToString()),
                    new Claim(ClaimTypesExt.LastName, user.LastName),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = false,

                    //todo move to config
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),

                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);


                return user;
            }


            // user authN failed
            return null;
            //return View();
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public async Task LogOut()
        {
            await HttpContext.SignOutAsync();
        }

        public IActionResult Index()
        {
            var viewModelWithUser = _controllerHelper_.ReturnViewModelWithUser(HttpContext);
            return View(viewModelWithUser);
        }

        private void CreateAdminUser()
        {
            _userLogicManager_.AddOrUpdate(
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
