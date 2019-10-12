using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcFrameworkWeb.Models;
using MvcFrameworkCml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFrameworkWeb.Services
{
    public class ControllerHelper
    {
        public BaseViewModel ReturnViewModelWithUser(HttpContext httpContext)
        {

            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
                return new BaseViewModel() { User = new EndUser(httpContext.User) };
            else
                return new BaseViewModel() { User = new EndUser() { Id = -1, Name = "Not logged in", LastName = "", Email = "", Role = "" } };
        }
    }
}
