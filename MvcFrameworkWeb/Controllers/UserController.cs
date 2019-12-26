using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcFrameworkBll;
using MvcFrameworkCml;
using MvcFrameworkCml.ViewModels;
using MvcFrameworkWeb.Services;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;

namespace MvcFrameworkWeb.Controllers
{
    public class UserController : CustomBaseController
    {
        private readonly UserLogicManager _userLogicManager_;
        public UserController(
            UserLogicManager userLogicManager,
            ControllerHelper controllerHelper,
            ILogger logger
            ) : base(controllerHelper, logger)
        {
            _userLogicManager_ = userLogicManager;
        }

        [Authorize(Roles = Role.Access.MUST_BE_ADMIN)]
        public async Task<ActionResult<EndUser>> GetUserById([FromUri]Int32 id)
        {
            var user = HttpContext.User;
            return await _userLogicManager_.GetAsync(id);
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public async Task<ActionResult<EndUser>> Me()
        {
            var idString = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypesExt.Id))?.Value;
            if (!String.IsNullOrWhiteSpace(idString) && Int32.TryParse(idString, out Int32 id))
                return await _userLogicManager_.GetAsync(id);
            else return null;
        }

        [AllowAnonymous]
        public async Task<ActionResult<Boolean>> Add([FromBody]EndUser user)
        {
            user.Id = 0;
            return await _userLogicManager_.AddAsync(user);
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public async Task<ActionResult<Boolean>> ChangeName([FromBody]RequestData<String> name)
        {
            var idString = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypesExt.Id))?.Value;
            if (!String.IsNullOrWhiteSpace(idString) && Int32.TryParse(idString, out Int32 id))
                return await _userLogicManager_.ChangeName(id, name.Data);
            return false;
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public async Task<ActionResult<Boolean>> ChangeLastName([FromBody]RequestData<String> lastName)
        {
            var idString = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypesExt.Id))?.Value;
            if (!String.IsNullOrWhiteSpace(idString) && Int32.TryParse(idString, out Int32 id))
                return await _userLogicManager_.ChangeLastName(id, lastName.Data);
            return false;
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public async Task<ActionResult<Boolean>> ChangeEmail([FromBody]RequestData<String> email)
        {
            var idString = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypesExt.Id))?.Value;
            if (!String.IsNullOrWhiteSpace(idString) && Int32.TryParse(idString, out Int32 id))
                return await _userLogicManager_.ChangeEmail(id, email.Data);
            return false;
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public async Task<ActionResult<Boolean>> ChangePassword([FromBody]RequestData<String> password)
        {
            var idString = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypesExt.Id))?.Value;
            if (!String.IsNullOrWhiteSpace(idString) && Int32.TryParse(idString, out Int32 id))
                return await _userLogicManager_.ChangePassword(id, password.Data);
            return false;
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