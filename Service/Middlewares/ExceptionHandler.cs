using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Model.Entities.Reports;
using Model.Exceptions;
using Service.ServiceClasses.ReportingServices;
using Service.ServiceInterfaces.PersonServices;
using Service.ServiceInterfaces.ReportingServices;
using System.Security.Claims;

namespace Service.Middlewares;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;
    public ExceptionHandler(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BaseException ex)
        {
            var exception = new ExceptionLog(ex, Guid.TryParse(context.User?.FindFirstValue(ClaimTypes.NameIdentifier), out Guid Id) ? Id : null);
            using (var scope = _serviceProvider.CreateScope())
            {            
                var _logger = scope.ServiceProvider.GetRequiredService<ILogger<ExceptionHandler>>();
                _logger.LogError("program error {@Exception}", exception);
                context.Response.Redirect(context.Request.PathBase + "/Home/error");

            }
        }
        catch (Exception ex)
        {
            var exception = new ExceptionLog(ex, Guid.TryParse(context.User?.FindFirstValue(ClaimTypes.NameIdentifier), out Guid Id) ? Id : null);
            using (var scope = _serviceProvider.CreateScope())
            {
                var _exceptionLogService = scope.ServiceProvider.GetRequiredService<IExceptionLogService>();
                await _exceptionLogService.SaveExceptionLog(exception);
                var _logger = scope.ServiceProvider.GetRequiredService<ILogger<ExceptionHandler>>();
                _logger.LogError("External error {@Exception}", exception);
                context.Response.Redirect(context.Request.PathBase + "/Home/error");
            }

        }
      
    }
}
