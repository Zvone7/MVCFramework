using Cml.DataModels;
using Cml.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Web.Services
{
    public class ControllerHelper
    {
        public BaseViewModel ReturnViewModelWithUser(HttpContext httpContext)
        {

            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
                return new BaseViewModel() { User = new EndUser(httpContext.User) };
            else
                return new BaseViewModel() { User = null };
        }
    }
}
