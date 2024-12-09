using DataTransferObject.DTOClasses.Order.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.ReportingServices;

public interface IViewLogService
{
    Task<int> GetTotalViews(CancellationToken cancellation);
    Task<List<ViewsResult>> GetDailyViews(CancellationToken cancellation, int DaysCount = 7);
}
