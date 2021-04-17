using System;
using MvcFrameworkCml.Infrastructure.Data;
using MvcFrameworkCml.Infrastructure.Startup;

namespace MvcFrameworkBll.Data
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IAppSettings _appSettings_;

        public ConnectionStringProvider(IAppSettings appSettings)
        {
            _appSettings_ = appSettings;
        }

        public String GetConnectionString()
        {
            return _appSettings_.ConnectionString;
        }
    }
}