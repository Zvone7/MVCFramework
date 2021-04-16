using System;
using MvcFrameworkCml.Logging;
using MvcFrameworkCml.Security;

namespace MvcFrameworkCml.Infrastructure.Startup
{
    public interface IAppSettings
    {
        Boolean UseMockedDb { get; set; }
        String Secret { get; set; }
        String ConnectionString { get; set; }
        Int32 CookieExpirePeriodInMins { get; set; }
        PasswordComplexitySettings PasswordComplexitySettings { get; set; }
        LoggingSettings LoggingSettings { get; set; }
    }
}
