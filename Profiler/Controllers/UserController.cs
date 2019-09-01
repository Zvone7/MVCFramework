using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profiler.Models;
using ProfilerLogic;
using ProfilerModels;
using System;
using System.Security.Claims;

namespace Profiler.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager _userManager;
        public UserController(UserManager userManager)
        {
            _userManager = userManager;
        }

        public ActionResult<Int32> GetUserByMail(String email)
        {
            if (email.Equals("admin")) { return 1; }
            else { return -1; }
        }

        public ActionResult<User> GetUserById(Int32 id)
        {
            return _userManager.GetUserById(id);
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("login", "login");
            }

            return returnUrl;
        }

        public ActionResult<User> LogIn([FromBody]UserLogin userLogin)
        {
            var user = _userManager.Authenticate(userLogin.Email, userLogin.Password);
            if (user != null)
            {
                //ClaimsIdentity identity;
                //identity = new ClaimsIdentity(new[] {
                //        //new Claim(ClaimTypes.Role, user.Role),
                //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //        new Claim(ClaimTypes.Name, user.ToString()),
                //        new Claim(ClaimTypes.GivenName, user.Email)
                //    }, "ApplicationCookie");

                ////HttpContext.

                //ControllerContext.HttpContext.GetOwinContext();
                //System.Web.Http..HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                //var ctx = Request.GetTypedHeaders();
                //var authManager = ctx.Authentication;
                //authManager.SignIn(identity);
                //if (model.ReturnUrl == null)
                //{
                //    model.ReturnUrl = "/Table/Table";
                //}

                //return Redirect(GetRedirectUrl(Url.RouteUrl.Url));
                //return ("Login");
                return user;
            }


            // user authN failed
            return null;
            //return View();
        }
    }
}