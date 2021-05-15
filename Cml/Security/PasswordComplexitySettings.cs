using System;
using System.Collections.Generic;

namespace Cml.Security
{
    public class PasswordComplexitySettings
    {
        public Int32 MinimumLength { get; set; }
        public List<RegexRule> Rules { get; set; }
    }

    public class RegexRule
    {
        public String Name { get; set; }
        public String Regex { get; set; }
    }
}
