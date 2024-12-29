using Infrastructure.Contracts.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Orders;
using Model.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Classes;

public class ViewLogRepository : BaseRepository<ViewLog, int>, IViewLogRepository
{
    public ViewLogRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
    public async Task<int> GetTotalViews(CancellationToken cancellation)
        => await _entitySet.CountAsync(cancellation);

    public async Task<(List<int> views, List<DateTime> dateTimes)> GetDailyViews(CancellationToken cancellation, int DaysCount = 7)
    {
        var startDate = DateTime.Now.AddDays(-DaysCount+1).Date;

        var query = await _entitySet
            .Where(x => x.DateTime > startDate)
            .GroupBy(x => x.DateTime.Date)
            .Select(group => new
            {
                views = group.Count(),
                Date = group.Key
            }).OrderBy(x => x.Date).ToListAsync(cancellation);

        var result = (views: query.Select(x => x.views).ToList(), dateTimes: query.Select(x => x.Date).ToList());
        return result;
    }

    public async Task SaveViewLogs(List<ViewLog> viewLogs)
    {
        await _entitySet.AddRangeAsync(viewLogs);
        await CommitAsync(CancellationToken.None);
    }
}




