using Microsoft.Extensions.Logging;

namespace MvcFrameworkBll
{
    public class CustomBaseLogicManager
    {
        protected readonly ILogger _logger_;

        public CustomBaseLogicManager(ILogger logger)
        {
            _logger_ = logger;
        }
    }
}
