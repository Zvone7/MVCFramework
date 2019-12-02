using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcFrameworkCml;
using MvcFrameworkCml.ViewModels;
using MvcFrameworkWeb.Services;
using System.Diagnostics;

namespace MvcFrameworkWeb.Controllers
{
    [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
    public class HomeController : CustomBaseController
    {
        public HomeController(ControllerHelper controllerHelper) : base(controllerHelper) { }

        public IActionResult Index()
        {
            return GetViewModelOrRedirect(HttpContext);
        }

        public IActionResult About()
        {
            return GetViewModelOrRedirect(HttpContext);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                User = HttpContext.User.Identity.IsAuthenticated ? new EndUser(HttpContext.User) : null
            }) ;
        }
    }
}
