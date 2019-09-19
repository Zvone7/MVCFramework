using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profiler.Models;
using ProfilerLogic;
using ProfilerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;

namespace Profiler.Controllers
{
    public class UserController : Controller
    {
        private readonly UserLogicManager _userLogicManager_;
        public UserController(UserLogicManager userLogicManager)
        {
            _userLogicManager_ = userLogicManager;
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
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

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = Role.Access.MUST_BE_AUTHENTICATED)]
        public ActionResult<Boolean> AddOrUpdate([FromBody]User user)
        {
            return _userLogicManager_.AddOrUpdate(user);
        }

                      
        public IActionResult Index()
        {
            var user = HttpContext.User;
            IList<String> userRoles = user.Claims.Where(x => x.Type.Equals(ClaimTypes.Role)).Select(y => y.Value).ToList();

            return View(userRoles);
        }
    }
}