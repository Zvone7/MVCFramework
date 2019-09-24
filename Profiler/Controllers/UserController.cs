using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profiler.Services;
using ProfilerLogic;
using ProfilerModels;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;

namespace Profiler.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserLogicManager _userLogicManager_;
        public UserController(UserLogicManager userLogicManager, ControllerHelper controllerHelper) : base(controllerHelper)
        {
            _userLogicManager_ = userLogicManager;
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public ActionResult<EndUser> GetUserByEmail([FromUri]String email)
        {
            return _userLogicManager_.Get(email);
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = Role.Access.MUST_BE_ADMIN)]
        public ActionResult<EndUser> GetUserById([FromUri]Int32 id)
        {
            var user = HttpContext.User;
            return _userLogicManager_.Get(id);
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public ActionResult<EndUser> Me()
        {
            var user = HttpContext.User;
            var email = user.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value;
            return _userLogicManager_.Get(email);
        }

        [AllowAnonymous]
        public ActionResult<Boolean> AddOrUpdate([FromBody]EndUser user)
        {
            var loggedInUserEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value;
            if (String.IsNullOrWhiteSpace(loggedInUserEmail))
                user.Id = 0;
            else
                user.Id = 1;
            return _userLogicManager_.AddOrUpdate(user);
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public IActionResult Index()
        {
            var model = _controllerHelper_.ReturnViewModelWithUser(HttpContext);

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            var viewModelWithUser = _controllerHelper_.ReturnViewModelWithUser(HttpContext);
            return View(viewModelWithUser);
        }
    }
}