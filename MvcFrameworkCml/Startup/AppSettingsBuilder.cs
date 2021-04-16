using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MvcFrameworkCml.Infrastructure.Startup;

namespace MvcFrameworkCml.Startup
{
    public static class AppSettingsBuilder
    {
        public static IAppSettings Build(String[] args = default)
        {
            try
            {
                String computerName = Environment.MachineName;
                var runtimeSettings = new Dictionary<String, String>
                    {{"App:ComputerName", computerName}};

                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()) //                                Priority Order            overrides:
                    .AddInMemoryCollection(runtimeSettings) //                                      add runtime properties.      
                    .AddJsonFile("appsettings.json") //                                             add default properties.      ↑
                    .AddEnvironmentVariables() //                                                   add environment vaiables.    ↑
                    .AddCommandLine(args ?? new String[] { }, MapJsonProperties()) //  add command line arguments.  ↑
                    .Build();

                IAppSettings appSettings = configuration.Get<AppSettings>();

                return appSettings;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private static Dictionary<String, String> MapJsonProperties()
        {
            return typeof(AppSettings)
                .GetProperties()
                .Select(prop => (mapped: $"-{prop.Name}", from: prop.Name))
                .ToDictionary(v => v.mapped, v => v.from);
        }
    }
}