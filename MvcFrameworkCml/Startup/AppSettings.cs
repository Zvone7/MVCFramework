using MvcFrameworkCml.Infrastructure;
using MvcFrameworkCml.Logging;
using MvcFrameworkCml.Security;
using System;
using MvcFrameworkCml.Infrastructure.Startup;

namespace MvcFrameworkCml.Startup
{
    public class AppSettings : IAppSettings
    {
        public Boolean UseMockedDb { get; set; }
        public String Secret { get; set; }
        public String ConnectionString { get; set; }
        public Int32 CookieExpirePeriodInMins { get; set; }
        public PasswordComplexitySettings PasswordComplexitySettings { get; set; }
        public LoggingSettings LoggingSettings { get; set; }
    }
}
