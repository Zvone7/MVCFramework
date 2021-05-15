using System;
using Cml.Infrastructure.Data;
using Cml.Infrastructure.Startup;

namespace Bll.Data
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