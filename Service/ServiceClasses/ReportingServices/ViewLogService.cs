using DataTransferObject.DTOClasses.Order.Results;
using Infrastructure.Contracts.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model.Entities.Orders;
using Model.Entities.Reports;
using Model.Exceptions;
using Service.ServiceInterfaces.PersonServices;
using Service.ServiceInterfaces.ReportingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses.ReportingServices;

public class ViewLogService : IViewLogService
{
    private readonly IViewLogRepository _viewRepository;
    private readonly IUserService _userService;
    public ViewLogService(IViewLogRepository viewRepository, IUserService userService)
    {
        _viewRepository = viewRepository;
        _userService = userService;
    }
    //تعداد کل ویوی سایت از اول
    public async Task<int> GetTotalViews(CancellationToken cancellation)
    {
        if (_userService.IsAdmin())
            return await _viewRepository.GetTotalViews(cancellation);
        throw new AccessDeniedException();
    }

    // لیست ویوی چند روز گذشته به صورت روزانه
    public async Task<List<ViewsResult>> GetDailyViews(CancellationToken cancellation, int DaysCount = 7)
    {
        if (!_userService.IsAdmin())
            throw new AccessDeniedException();

        var result = await _viewRepository.GetDailyViews(cancellation, DaysCount);

        return result.dateTimes.Select((datetime, index) =>
         new ViewsResult
         {
             DateTime = datetime,
             view = result.views[index]
         }).ToList();

    }

}





