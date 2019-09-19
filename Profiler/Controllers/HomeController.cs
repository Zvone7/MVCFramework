using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profiler.Models;
using ProfilerModels;

namespace Profiler.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
    //[AllowAnonymous]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var viewModelWithUser = _controllerHelper_.ReturnViewModelWithUser(HttpContext);
            return View(viewModelWithUser);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var viewModelWithUser = _controllerHelper_.ReturnViewModelWithUser(HttpContext);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                User = viewModelWithUser.User });
        }
    }
}
