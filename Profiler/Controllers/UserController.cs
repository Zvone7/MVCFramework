using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profiler.Services;
using ProfilerLogic;
using ProfilerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;

namespace Profiler.Controllers
{
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
    public class UserController : BaseController
    {
        private readonly UserLogicManager _userLogicManager_;
        public UserController(UserLogicManager userLogicManager, ControllerHelper controllerHelper) : base(controllerHelper)
        {
            _userLogicManager_ = userLogicManager;
        }

        public ActionResult<User> GetUserByEmail([FromUri]String email)
        {
            return _userLogicManager_.Get(email);
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = Role.Access.MUST_BE_ADMIN)]
        public ActionResult<User> GetUserById([FromUri]Int32 id)
        {
            var user = HttpContext.User;
            return _userLogicManager_.Get(id);
        }

        public ActionResult<User> Me()
        {
            var user = HttpContext.User;
            var email = user.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email)).Value;
            return _userLogicManager_.Get(email);
        }

        public ActionResult<Boolean> AddOrUpdate([FromBody]User user)
        {
            return _userLogicManager_.AddOrUpdate(user);
        }


        public IActionResult Index()
        {
            var model = _controllerHelper_.ReturnViewModelWithUser(HttpContext);

            return View(model);
        }
    }
}