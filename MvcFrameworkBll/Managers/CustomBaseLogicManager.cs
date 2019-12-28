using Microsoft.Extensions.Logging;
using MvcFrameworkCml.Infrastructure;

namespace MvcFrameworkBll.Managers
{
    public class CustomBaseLogicManager
    {
        protected readonly IAppSettings _appSettings_;
        protected readonly ILogger _logger_;

        public CustomBaseLogicManager(IAppSettings settings, ILogger logger)
        {
            _appSettings_ = settings;
            _logger_ = logger;
        }
    }
}
