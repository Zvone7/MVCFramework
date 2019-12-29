using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcFrameworkCml;
using MvcFrameworkCml.Infrastructure;
using MvcFrameworkWeb.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace MvcFrameworkWeb.Controllers
{
    public class TestController : CustomBaseController
    {
        public TestController(
            ControllerHelper controllerHelper,
            IAppSettings appSettings,
            ILogger logger
            ) : base(controllerHelper, appSettings, logger) { }

        [System.Web.Http.HttpGet]
        public ActionResult<Int32> Testing()
        {
            return 5;
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        [Microsoft.AspNetCore.Mvc.Route("Test/AuthorizedEndpoint")]
        public ActionResult<String> AuthorizedEndpoint([FromUri]String value)
        {
            return $"AUTHORIZED: Returning value {value}";
        }

        public ActionResult<String> UnuthorizedEndpoint([FromUri]String value)
        {
            var user = HttpContext.User;
            return $"UNAUTHORIZED: Returning value {value}";
        }

        public async Task<ActionResult<String>> Test1([FromUri]String value)
        {
            if (!String.IsNullOrWhiteSpace(value)) return value;
            else return "No value";

        }

        public async Task<ActionResult<String>> SetAdminRole([FromUri]String value)
        {
            if (!String.IsNullOrWhiteSpace(value)) return value;
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, Role.ADMIN)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,

                    //todo move to config
                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),

                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                var user = HttpContext.User;
                return "success";
            }
            catch (Exception e)
            {
                return "fail." + e.Message;
            }
        }
        public IActionResult Index()
        {
            return GetViewModelOrRedirect(HttpContext);
        }

    }
}