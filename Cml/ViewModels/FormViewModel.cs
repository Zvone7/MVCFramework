using System;
using Cml.DataModels;

namespace Cml.ViewModels
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
