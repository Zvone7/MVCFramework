using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profiler.Models;
using ProfilerLogic;
using ProfilerModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;

namespace Profiler.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager _userManager;
        private ClaimsPrincipal claimsPrincipal;
        public UserController(UserManager userManager, IPrincipal principal)
        {
            _userManager = userManager;
            claimsPrincipal = (ClaimsPrincipal)principal;

        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = Role.User + "," + Role.Admin)]
        public ActionResult<User> GetUserByEmail([FromUri]String email)
        {
            var user = HttpContext.User;
            var stuff = Thread.CurrentPrincipal;
            return _userManager.Get(email);
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = Role.Admin)]
        public ActionResult<User> GetUserById([FromUri]Int32 id)
        {
            return _userManager.Get(id);
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = Role.User + "," + Role.Admin)]
        public ActionResult<Boolean> AddOrUpdate([FromBody]User user)
        {
            return _userManager.AddOrUpdate(user);
        }

        public async Task LogOut()
        {
            await HttpContext.SignOutAsync();
        }

        public async Task<ActionResult<User>> LogIn([FromBody]UserLoginData userLogin)
        {
            var user = _userManager.Authenticate(userLogin.Email, userLogin.Password);
            if (user != null)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    //new Claim("FullName", user.Name+" "+user.LastName),
                    new Claim(ClaimTypes.Role, user.Role),
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
    }
}