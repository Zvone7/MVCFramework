using Microsoft.AspNetCore.Mvc;
using Profiler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profiler.Controllers
{
    public class BaseController : Controller
    {

        protected readonly ControllerHelper _controllerHelper_;
        public BaseController(ControllerHelper controllerHelper)
        {
            _controllerHelper_ = controllerHelper;
        }
    }
}
