using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MvcFrameworkBll.Data;
using MvcFrameworkBll.Managers;
using MvcFrameworkCml.Infrastructure.Data;
using MvcFrameworkCml.Infrastructure.Managers;
using MvcFrameworkCml.Infrastructure.Repository;
using MvcFrameworkCml.Infrastructure.Startup;
using MvcFrameworkDbl;
using MvcFrameworkWeb.Services;

namespace MvcFrameworkWeb
{
    public partial class Startup
    {
        public async Task AddBusinesLogicAsync(IServiceCollection services, IConfiguration configuration, IHostEnvironment env, IAppSettings appSettings)
        {
            // COOKIES
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                options =>
                {
                    options.LoginPath = "/Login";
                    options.AccessDeniedPath = "/Login";
                    options.Cookie.Name = "UserLoginCookie";
                    options.Cookie.HttpOnly = false;
                    //MinimumSameSitePolicy = SameSiteMode.Strict,
                });


            // LOGGING
            var minimumLoggingLevel = LogLevel.Information;
            if (!String.IsNullOrWhiteSpace(appSettings.LoggingSettings.DefaultLogLevel) &&
                Enum.TryParse(appSettings.LoggingSettings.DefaultLogLevel, out LogLevel minimumLogingLevelConfigValue))
                minimumLoggingLevel = minimumLogingLevelConfigValue;
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(minimumLoggingLevel);
            });

            // DI
            services.AddSingleton(x => new ControllerHelper());
            services.AddSingleton(x => x.GetService<ILoggerFactory>().CreateLogger("MvcFramework"));
                
            services.AddSingleton<IConnectionStringProvider,ConnectionStringProvider>();
            
            if (appSettings.UseMockedDb)
            {
                services.AddSingleton<IMockedDataProvider, MockedDataProvider>();
                services.AddSingleton<IEndUserRepository, MockedEndUserRepository>();
            }
            else
            {
                services.AddSingleton<IEndUserRepository, EndUserRepository>();
            }

            services.AddSingleton<IEndUserManager, EndUserManager>();
        }
    }
}