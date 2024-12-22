using Microsoft.AspNetCore.Http;
using Model.Entities.Reports;
using Service.ServiceInterfaces.PersonServices;
using Service.ServiceInterfaces.ReportingServices;
using System.Security.Claims;

namespace Service.Middlewares;

public class RequestLogger
{
    private readonly RequestDelegate _next;
    private readonly IHangfireServices _viewLogService;
   
    public RequestLogger(RequestDelegate next, IHangfireServices viewLogService)
    {
        _next = next;
        _viewLogService = viewLogService;

    }

    public async Task InvokeAsync(HttpContext context)
    {
        Guid? requester= Guid.TryParse(context.User?.FindFirstValue(ClaimTypes.NameIdentifier), out Guid requesterId)? requesterId:null;     
        
        _viewLogService.AddViewLog(new ViewLog()
        {
            IP = context.Connection.RemoteIpAddress?.ToString(),
            DateTime = DateTime.Now,
            Url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}",
            userId = requester
        });

        await _next(context);
    }
}
