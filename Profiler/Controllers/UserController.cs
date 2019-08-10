using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfilerLogic;
using ProfilerModels;
using System;
using System.Security.Claims;

namespace Profiler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager _userManager;
        public UserController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetUserByUsername")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Int32> GetUserByUsername(String username)
        {
            if (username.Equals("admin")) { return 1; }
            else { return -1; }
        }

        [HttpGet]
        [Route("GetUserById")]
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

        //[AllowAnonymous]
        //[HttpPost("authenticate")]
        //[HttpPost]
        [Route("Authenticate")]
        public ActionResult Authenticate([FromBody]String username, String password)
        {
            //var user = _userService.Authenticate(userParam.Username, userParam.Password);
            var user = _userManager.GetUserById(1);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }



        [HttpPost]
        [Route("LogIn")]
        public ActionResult<String> LogIn([FromBody]String username, String password)
        {
            //var _userLogic = new UserLogic();
            //MyUserDto user = _userLogic.TryLogin(model);
            User user = null;
            if (user != null)
            {
                ClaimsIdentity identity;
                identity = new ClaimsIdentity(new[] {
                        //new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name+" "+user.LastName),
                        new Claim(ClaimTypes.GivenName, user.Username)
                    }, "ApplicationCookie");

                //HttpContext.

                //ControllerContext.HttpContext.GetOwinContext();
                //System.Web.Http..HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var ctx = Request.GetTypedHeaders();
                //var authManager = ctx.Authentication;
                //authManager.SignIn(identity);
                //if (model.ReturnUrl == null)
                //{
                //    model.ReturnUrl = "/Table/Table";
                //}

                //return Redirect(GetRedirectUrl(Url.RouteUrl.Url));
                return ("Login");
            }


            // user authN failed
            return ("Invalid username or password");
            //return View();
        }
    }
}