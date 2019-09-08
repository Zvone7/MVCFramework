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
using System.Web;
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

        public ActionResult<User> LogIn([FromBody]UserLoginData userLogin)
        {
            var user = _userManager.Authenticate(userLogin.Email, userLogin.Password);
            if (user != null)
            {
                var identity = new GenericIdentity(user.Email);
                //claimsPrincipal.Identity = identity;
                identity.AddClaim(new Claim("Role", Role.Admin));

                var threadPrincipal = new ClaimsPrincipal(identity);


                //var principal = Thread.CurrentPrincipal;

                Thread.CurrentPrincipal = threadPrincipal;


                //var currentContext = System.Web.HttpContext.Current;

                HttpContext.User = threadPrincipal;


                ApplicationUser user = 
                    System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                return user;
            }


            // user authN failed
            return null;
            //return View();
        }
    }
}