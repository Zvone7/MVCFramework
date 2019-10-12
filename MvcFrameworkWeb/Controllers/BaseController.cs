using Microsoft.AspNetCore.Mvc;
using MvcFrameworkWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFrameworkWeb.Controllers
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
