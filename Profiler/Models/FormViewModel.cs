﻿using ProfilerModels;
using System;

namespace Profiler.Models
{
    public class FormViewModel
    {
        public FormFields Fields { get; set; }
        public UserLoginData UserLoginData { get; set; }
        public User User { get; set; }
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
}
