﻿using Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Service.ServiceClasses;
using Model.Entities.Person;
using Infrastructure.SeedData;
using Infrastructure.Repository;
using Infrastructure.ChangeInterceptors;
using Infrastructure.Contracts.Repository;
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
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DbContext, AppDbContext>(opt =>
        {
            opt.AddInterceptors(new ChangeInterceptor());
            opt.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContextConnection"));
        });

        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<ICachedData, CachedData>();


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

        builder.Services.AddMvc();
        builder.Services.AddControllers();
        builder.Services.AddRazorPages();

        builder.Services.AddAuthentication();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ClaimsPrincipal>(x =>
        {
            var httpContextAccessor = x.GetService<IHttpContextAccessor>();
            return httpContextAccessor?.HttpContext?.User ?? new ClaimsPrincipal();
        });

        builder.Services.RegisterRepositories();
        builder.Services.RegisterServices();

        builder.Services.AddSingleton<IHangfireServices, HangfireServices>();


        builder.Host.ConfigureLogging((context, logger) =>
        {

            logger.ClearProviders();
            logger.AddConsole();


        }).UseSerilog((context, logger) =>
        {
            logger.MinimumLevel.Information();
            logger.Enrich.WithThreadId();
            logger.Enrich.WithEnvironmentName();
            logger.WriteTo.Console();
            logger.WriteTo.Seq("http://localhost:5341/");
        });



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



        TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
        MapsterConfig.RegisterMapping();

        var app = builder.Build();
        app.UseMiddleware<ExceptionHandler>();

        app.UseHangfireDashboard("/hangfire");

        var options = new RecurringJobOptions
        {
            TimeZone = TimeZoneInfo.Local
        };

        RecurringJob.AddOrUpdate<IHangfireServices>(
            service => service.SaveViewLogs(),
            "*/10 * * * *",
            options: options
        );


        RecurringJob.AddOrUpdate<IHangfireServices>(
            service => service.SavePrpductViewLogs(),
            "*/10 * * * *",
                options: options
        );


        app.UseMiddleware<RequestLogger>();


        using (var scope = app.Services.CreateScope())
        {
            await IdentitySeedData.AddIdentityData(scope.ServiceProvider);
        }

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "bin", "Debug", "net8.0", "upload")),
            RequestPath = "/upload"
        });


        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoint =>
        {
            endpoint.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            endpoint.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoint.MapRazorPages();
        });

        app.Run();
    }
}



