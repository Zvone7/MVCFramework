using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProfilerLogic;
using ProfilerModels;

namespace Profiler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager _userManager;
        public UserController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetUserByUsername")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Int32> GetUserByUsername(String username)
        {
            if (username.Equals("admin")) { return 1; }
            else { return -1; }
        }

        [HttpGet]
        [Route("GetUserById")]
        public ActionResult<User> GetUserById(Int32 id)
        {
            return _userManager.GetUserById(id);
        }

        //eg: GET api/users/{id}/friends
        //[HttpGet]
        //[Route("{id:guid}/friends")]
        //public IEnumerable<User> Friends(Guid id) { ...}

    }
}