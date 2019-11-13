using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcFrameworkCml;
using MvcFrameworkWeb.Models;
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
            var viewModelWithUser = _controllerHelper_.ReturnViewModelWithUser(HttpContext);
            return View(viewModelWithUser);
        }

        public IActionResult About()
        {
            var viewModelWithUser = _controllerHelper_.ReturnViewModelWithUser(HttpContext);
            return View(viewModelWithUser);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var viewModelWithUser = _controllerHelper_.ReturnViewModelWithUser(HttpContext);
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                User = viewModelWithUser.User
            });
        }
    }
}
