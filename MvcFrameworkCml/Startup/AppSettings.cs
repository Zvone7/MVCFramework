using MvcFrameworkCml.Infrastructure;
using MvcFrameworkCml.Logging;
using MvcFrameworkCml.Security;
using System;

namespace MvcFrameworkCml.Startup
{
    public class AppSettings : IAppSettings
    {
        public string Secret { get; set; }
        public string ConnectionString { get; set; }
        public Int32 CookieExpirePeriodInMins { get; set; }
        public PasswordComplexitySettings PasswordComplexitySettings { get; set; }
        public LoggingSettings LoggingSettings { get; set; }
    }
}
