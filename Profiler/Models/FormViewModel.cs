using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profiler.Models
{
    public class FormViewModel
    {
        public FormFields Fields { get; set; }
        public UserLoginData UserLoginData { get; set; }
        public UserRegisterData UserRegisterData { get; set; }
    }

    public class FormFields
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
    }

    public class UserLoginData
    {
        public String Email { get; set; }
        public String Password { get; set; }
    }

    public class UserRegisterData
    {
        public String Email { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public String Password { get; set; }
    }
}
