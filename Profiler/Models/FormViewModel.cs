using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profiler.Models
{
    public class FormViewModel
    {
        public FormFields Fields { get; set; }
        public UserLogin UserLogin { get; set; }
    }

    public class FormFields
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
    }

    public class UserLogin
    {
        public String Email { get; set; }
        public String Password { get; set; }
    }
}
