using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcFrameworkCml;
using MvcFrameworkCml.ViewModels;
using MvcFrameworkWeb.Services;
using System;
using System.Linq;

namespace MvcFrameworkWeb.Controllers
{
    public class CustomBaseController : Controller
    {

        protected readonly ControllerHelper _controllerHelper_;
        public CustomBaseController(ControllerHelper controllerHelper)
        {
            _controllerHelper_ = controllerHelper;
        }

        protected IActionResult GetViewModelOrRedirect(HttpContext httpContext)
        {

            if (httpContext.User.Identity.IsAuthenticated)
                return View(new BaseViewModel() { User = new EndUser(httpContext.User) });
            else
            {
                if (httpContext.Request.Path.HasValue && httpContext.Request.Path.Value.ToLower().Contains("login"))
                    return View();
                var action = GetRedirectAction(httpContext);
                return RedirectToAction(action.Item1, action.Item2);
            }
        }

        private (String, String) GetRedirectAction(HttpContext httpContext)
        {
            var idClaimValue = httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypesExt.Id))?.Value;
            if (Int32.TryParse(idClaimValue, out Int32 id) && id > 0)
            {
                return ("Index", "Home");
            }
            return ("Index", "Login");
        }
    }
}
