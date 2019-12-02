using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcFrameworkBll;
using MvcFrameworkCml;
using MvcFrameworkCml.ViewModels;
using MvcFrameworkWeb.Services;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;

namespace MvcFrameworkWeb.Controllers
{
    public class UserController : CustomBaseController
    {
        private readonly UserLogicManager _userLogicManager_;
        public UserController(UserLogicManager userLogicManager, ControllerHelper controllerHelper) : base(controllerHelper)
        {
            _userLogicManager_ = userLogicManager;
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public ActionResult<EndUser> GetUserByEmail([FromUri]String email)
        {
            return _userLogicManager_.Get(email);
        }

        [Authorize(Roles = Role.Access.MUST_BE_ADMIN)]
        public ActionResult<EndUser> GetUserById([FromUri]Int32 id)
        {
            var user = HttpContext.User;
            return _userLogicManager_.Get(id);
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public ActionResult<EndUser> Me()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value;
            return _userLogicManager_.Get(email);
        }

        [AllowAnonymous]
        public ActionResult<Boolean> Add([FromBody]EndUser user)
        {
            var loggedInUserEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value;
            if (String.IsNullOrWhiteSpace(loggedInUserEmail))
                user.Id = 0;
            else
                user.Id = 1;
            return _userLogicManager_.Add(user);
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public ActionResult<Boolean> ChangeName([FromBody]RequestData<String> name)
        {
            var user = HttpContext.User;
            var email = user.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value;
            return _userLogicManager_.ChangeName(email, name.Data);
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public ActionResult<Boolean> ChangeLastName([FromBody]RequestData<String> lastName)
        {
            var user = HttpContext.User;
            var email = user.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value;
            return _userLogicManager_.ChangeLastName(email, lastName.Data);
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public ActionResult<Boolean> ChangeEmail([FromBody]RequestData<String> email)
        {
            var user = HttpContext.User;
            var activeEmail = user.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value;
            return _userLogicManager_.ChangeLastName(activeEmail, email.Data);
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public ActionResult<Boolean> ChangePassword([FromBody]RequestData<String> password)
        {
            var user = HttpContext.User;
            var email = user.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value;
            return _userLogicManager_.ChangePassword(email, password.Data);
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public IActionResult Index()
        {
            return GetViewModelOrRedirect(HttpContext);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return GetViewModelOrRedirect(HttpContext);
        }
    }
}