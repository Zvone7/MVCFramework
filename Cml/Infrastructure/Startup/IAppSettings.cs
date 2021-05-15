using System;
using Cml.Logging;
using Cml.Security;

namespace Cml.Infrastructure.Startup
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
