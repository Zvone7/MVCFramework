using System;
using System.Security.Principal;
using Bll.Data;
using Bll.Managers;
using Cml.Infrastructure.Data;
using Cml.Infrastructure.Managers;
using Cml.Infrastructure.Repository;
using Cml.Infrastructure.Startup;
using Cml.Startup;
using Dbl;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Web.Services;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Web
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IHostEnvironment _env;

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

                // APPSETTINGS
                var appSettings = AppSettingsBuilder.Build();
                services.AddSingleton(x => appSettings);

                AddBusinesLogic(services, Configuration, _env, appSettings);
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
        
        void AddBusinesLogic(IServiceCollection services, IConfiguration configuration, IHostEnvironment env, IAppSettings appSettings)
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
            services.AddSingleton(x => x.GetService<ILoggerFactory>().CreateLogger("MVC5"));
                
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