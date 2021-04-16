using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MvcFrameworkBll.Managers;
using MvcFrameworkCml.Infrastructure.Managers;
using MvcFrameworkCml.Infrastructure.Repository;
using MvcFrameworkCml.Startup;
using MvcFrameworkDbl;
using MvcFrameworkWeb.Services;
using System;
using System.Security.Principal;

namespace MvcFrameworkWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

                services.AddMvc(opt => { opt.EnableEndpointRouting = true; });
                services.AddControllersWithViews().AddRazorRuntimeCompilation();
                
                services.AddControllers();
                services.AddRazorPages();
                 services.AddCors();
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

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

                // APPSETTINGS
                var appSettings = AppSettingsBuilder.Build();
                services.AddScoped(x => appSettings);

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
                services.AddScoped(x => new ControllerHelper());
                services.AddScoped(x => x.GetService<ILoggerFactory>().CreateLogger("MvcFramework"));
                if (appSettings.UseMockedDb)
                {
                    services.AddScoped<IMockedDataProvider, MockedDataProvider>();
                    services.AddScoped<IEndUserRepository, MockedEndUserRepository>();
                }
                else
                {
                    services.AddScoped<IEndUserRepository, EndUserRepository>();
                }

                services.AddScoped<IEndUserManager, EndUserManager>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                Console.WriteLine("Running on development.");
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
             app.UseHttpsRedirection();
            app.UseStaticFiles();
             app.UseCookiePolicy();
            app.UseRouting();
             app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}"); 
                endpoints.MapControllers();
            });

        }
    }
}