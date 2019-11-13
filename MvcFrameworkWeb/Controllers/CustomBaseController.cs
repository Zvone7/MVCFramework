using Microsoft.AspNetCore.Mvc;
using MvcFrameworkWeb.Services;

namespace MvcFrameworkWeb.Controllers
{
    public class CustomBaseController : Controller
    {

        protected readonly ControllerHelper _controllerHelper_;
        public CustomBaseController(ControllerHelper controllerHelper)
        {
            _controllerHelper_ = controllerHelper;
        }
    }
}
