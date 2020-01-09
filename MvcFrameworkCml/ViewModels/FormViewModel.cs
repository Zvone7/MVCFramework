using MvcFrameworkCml;
using System;

namespace MvcFrameworkCml.ViewModels
{
    public class FormViewModel
    {
        public UserLoginData UserLoginData { get; set; }
        public EndUser User { get; set; }
    }

    public class UserLoginData
    {
        public String Email { get; set; }
        public String Password { get; set; }
    }
}
