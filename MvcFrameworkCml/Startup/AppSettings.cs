using MvcFrameworkCml.Infrastructure;
using MvcFrameworkCml.Logging;

namespace MvcFrameworkCml.Startup
{
    public class AppSettings : IAppSettings
    {
        public string Secret { get; set; }
        public string ConnectionString { get; set; }
        public LoggingSettings LoggingSettings { get; set; }
    }
}
