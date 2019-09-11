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
using System.Web;
using System.Web.Http;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
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

        public ActionResult<Boolean> AddOrUpdate([FromBody]User user)
        {
            return _userManager.AddOrUpdate(user);
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("login", "login");
            }

            return returnUrl;
        }
        public async Task LogOut()
        {
            //var userPrincipal = Context.Principal;

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
                    new Claim("FullName", user.Name+" "+user.LastName),
                    new Claim(ClaimTypes.Role, Role.Admin),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = false,
                    // Refreshing the authentication session should be allowed.

                    ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(60),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);





                //var identity = new GenericIdentity(user.Email);
                ////claimsPrincipal.Identity = identity;
                //identity.AddClaim(new Claim("Role", Role.Admin));

                //var threadPrincipal = new ClaimsPrincipal(identity);


                ////var principal = Thread.CurrentPrincipal;

                //Thread.CurrentPrincipal = threadPrincipal;

                //UserManager
                ////var currentContext = System.Web.HttpContext.Current;

                //HttpContext.User = threadPrincipal;


                //ApplicationUser user = 
                //    System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                return user;
            }


            // user authN failed
            return null;
            //return View();
        }
    }
}