using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcFrameworkCml;
using MvcFrameworkCml.Infrastructure;
using MvcFrameworkCml.Infrastructure.Managers;
using MvcFrameworkCml.Transfer;
using MvcFrameworkWeb.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using MvcFrameworkCml.DataModels;
using MvcFrameworkCml.Infrastructure.Startup;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;

namespace MvcFrameworkWeb.Controllers
{
    public class UserController : CustomBaseController
    {
        private readonly IEndUserManager _endUserManager_;
        public UserController(
            IEndUserManager endUserManager,
            ControllerHelper controllerHelper,
            IAppSettings appSettings,
            ILogger logger
            ) : base(controllerHelper, appSettings, logger)
        {
            _endUserManager_ = endUserManager;
        }

        [Authorize(Roles = Role.Access.MUST_BE_ADMIN)]
        public async Task<ActionResult<Content<EndUser>>> GetUserById([FromUri]Int32 id)
        {
            return await _endUserManager_.GetEntityAsync(id);
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public async Task<ActionResult<Content<EndUser>>> Me()
        {
            var result = new Content<EndUser>();
            var idString = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypesExt.Id))?.Value;
            if (!String.IsNullOrWhiteSpace(idString) && Int32.TryParse(idString, out Int32 id))
                result = await _endUserManager_.GetEntityAsync(id);
            else
                result.AppendError(new ArgumentNullException(), "Id not found");

            return result;
        }

        [AllowAnonymous]
        public async Task<ActionResult<Content<Boolean>>> RegisterSelf([FromBody]EndUser user)
        {
            user.Id = 0;
            var result = await _endUserManager_.AddEntityAsync(user);
            return result;
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public async Task<ActionResult<Content<Boolean>>> UpdateUser([FromBody]EndUser user)
        {
            var resultContent = new Content<Boolean>();
            var idString = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypesExt.Id))?.Value;
            if (!String.IsNullOrWhiteSpace(idString) && Int32.TryParse(idString, out Int32 id))
            {
                user.Id = id;
                var result = await _endUserManager_.UpdateEntityAsync(user);
                if (result.HasError) resultContent.AppendError(result);
                else resultContent.SetData(result.Data);
            }
            else
            {
                resultContent.AppendError(new ArgumentException(), "User not signed in properly.");
            }
            return resultContent;
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public async Task<ActionResult<Content<Boolean>>> UpdateUserEmail([FromBody]EndUser user)
        {
            var resultContent = new Content<Boolean>();
            var idString = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypesExt.Id))?.Value;
            if (!String.IsNullOrWhiteSpace(idString) && Int32.TryParse(idString, out Int32 id))
            {
                var result = await _endUserManager_.UpdateUserEmailAsync(id, user.Email);
                if (result.HasError) resultContent.AppendError(result);
                else resultContent.SetData(result.Data);
            }
            else
            {
                resultContent.AppendError(new ArgumentException(), "User not signed in properly.");
            }
            return resultContent;
        }

        [Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public async Task<ActionResult<Content<Boolean>>> UpdateUserPassword([FromBody]EndUser user)
        {
            var resultContent = new Content<Boolean>();
            var idString = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypesExt.Id))?.Value;
            if (!String.IsNullOrWhiteSpace(idString) && Int32.TryParse(idString, out Int32 id))
            {
                var result = await _endUserManager_.UpdateUserPasswordAsync(id, user.Password);
                if (result.HasError) resultContent.AppendError(result);
                else resultContent.SetData(result.Data);
            }
            else
            {
                resultContent.AppendError(new ArgumentException(), "User not signed in properly.");
            }
            return resultContent;
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