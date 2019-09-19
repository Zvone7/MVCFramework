using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profiler.Models;
using ProfilerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profiler.Services
{
    public class ControllerHelper
    {
        public BaseViewModel ReturnViewModelWithUser(HttpContext httpContext)
        {

            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
                return new BaseViewModel() { User = new User(httpContext.User) };
            else
                return new BaseViewModel() { User = new User() { Id = -1, Name = "Not logged in", LastName = "", Email = "", Role = "" } };
        }
    }
}
