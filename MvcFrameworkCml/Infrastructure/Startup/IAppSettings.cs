using MvcFrameworkCml.Logging;
using System;

namespace MvcFrameworkCml.Infrastructure
{
    public interface IAppSettings
    {
        String Secret { get; set; }
        String ConnectionString { get; set; }
        Int32 CookieExpirePeriodInMins { get; set; }
        LoggingSettings LoggingSettings { get; set; }
    }
}
