using Model.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contracts.Repository;

public interface IViewLogRepository:IBaseRepository<ViewLog,int>
{
    Task<int> GetTotalViews(CancellationToken cancellation);
    Task<(List<int> views, List<DateTime> dateTimes)> GetDailyViews(CancellationToken cancellation, int DaysCount = 7);
}
