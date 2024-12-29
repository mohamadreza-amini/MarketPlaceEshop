using Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Service.ServiceClasses;
using Model.Entities.Person;
using Infrastructure.ChangeInterceptors;
using Infrastructure.Contracts.Cache;
using Infrastructure.Cache;
using Microsoft.Extensions.FileProviders;
using Serilog;
using Hangfire;
using Hangfire.SqlServer;
using Service.ServiceInterfaces.ReportingServices;
using Service.Middlewares;

namespace MarketPlaceEshop;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region DbContext Configuration
        builder.Services.AddDbContext<DbContext, AppDbContext>(opt =>
        {
            opt.AddInterceptors(new ChangeInterceptor());
            opt.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContextConnection"));
        });
        #endregion

        #region Cache configuration
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<ICachedData, CachedData>();
        #endregion

        #region Identity configuration
        builder.Services.AddIdentity<User, Role>(option =>
        {
            option.Password.RequireDigit = false;
            option.Password.RequireUppercase = false;
            option.Password.RequireNonAlphanumeric = false;
            option.Password.RequireLowercase = false;
            option.Password.RequiredLength = 6;
            option.User.RequireUniqueEmail = true;

        }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

        builder.Services.AddScoped<ClaimsPrincipal>();


        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/account/login";
            options.AccessDeniedPath = "/Home/error";
            options.Events.OnRedirectToLogin = context =>
            {
                var path = context.Request.Path;
                if (path.StartsWithSegments("/admin"))
                {
                    context.Response.Redirect("/admin/account/login");
                }
                else if (path.StartsWithSegments("/supplier"))
                {
                    context.Response.Redirect("/supplier/account/login");
                }
                else
                {
                    context.Response.Redirect("/account/login");
                }
                return Task.CompletedTask;
            };
        });

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<ClaimsPrincipal>(x =>
        {
            var httpContextAccessor = x.GetService<IHttpContextAccessor>();
            return httpContextAccessor?.HttpContext?.User ?? new ClaimsPrincipal();
        });

        builder.Services.AddAuthentication();

        #endregion

        builder.Services.AddMvc();
        builder.Services.AddControllers();
        builder.Services.AddRazorPages();

        //Register Repositories
        builder.Services.RegisterRepositories();
        //Register Services
        builder.Services.RegisterServices();

        #region Logging configuration
        
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        builder.Host.UseSerilog((context, logger) =>
        {
            logger.MinimumLevel.Information();
            logger.Enrich.WithThreadId();
            logger.Enrich.WithEnvironmentName();
            logger.WriteTo.Console();
            logger.WriteTo.Seq("http://localhost:5341/");
        });
        #endregion

        #region Configure background tasks 

        builder.Services.AddSingleton<IHangfireServices, HangfireServices>();

        builder.Services.AddHangfire(config =>
        {
            config.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.FromSeconds(15),
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            });
        });

        builder.Services.AddHangfireServer();


        #endregion

        #region Mapster configuration
        TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
        MapsterConfig.RegisterMapping();
        #endregion

        var app = builder.Build();

        app.UseMiddleware<ExceptionHandler>();

        app.UseMiddleware<RequestLogger>();

        #region Add background work
        app.UseHangfireDashboard("/hangfire");

        var options = new RecurringJobOptions
        {
            TimeZone = TimeZoneInfo.Local
        };

        RecurringJob.AddOrUpdate<IHangfireServices>(
            "SaveViewLogsJob",
            service => service.SaveViewLogs(),
            "*/10 * * * *",
            options: options
        );

        RecurringJob.AddOrUpdate<IHangfireServices>(
            "SaveProductViewLogs",
            service => service.SaveProductViewLogs(),
            "*/10 * * * *",
                options: options
        );
        #endregion

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        #region Static File Configuration

        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "bin", "Debug", "net8.0", "upload");
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(uploadPath),
            RequestPath = "/upload"
        });
        #endregion

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
             name: "areas",
             pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        app.Run();
    }
}



