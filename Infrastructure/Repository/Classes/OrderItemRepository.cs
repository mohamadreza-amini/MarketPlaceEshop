using Infrastructure.Contracts.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Classes;

public class OrderItemRepository : BaseRepository<OrderItem, Guid>, IOrderItemRepository
{
    public OrderItemRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<(List<decimal> totalSales, List<decimal> totalDiscount, List<DateTime> dateTimes)> GetDailySales(CancellationToken cancellation, Expression<Func<OrderItem, bool>> predicate = null, int DaysCount = 7)
    {
        if (predicate == null)
            predicate = x => true;

        var startDate = DateTime.Now.AddDays(-DaysCount);

        var query = await _entitySet
            .Where(predicate)
            .Where(x => x.Order.OrderDate.Date >= startDate)
            .GroupBy(x => x.Order.OrderDate.Date)
            .Select(group => new
            {
                Date = group.Key,
                TotalSales = group.Sum(x => x.Quantity * x.ProductSupplier.Prices.Where(p => p.ExpiredTime == null).Select(p => p.PriceValue).FirstOrDefault()),
                TotalDiscount = group.Sum(x => x.Quantity * x.UnitDiscount)
            })
            .OrderBy(x => x.Date)
            .ToListAsync(cancellation);


        var result = (
            totalSales: query.Select(x => x.TotalSales).ToList(),
            totalDiscount: query.Select(x => x.TotalDiscount).ToList(),
            dateTimes: query.Select(x => x.Date).ToList()
        );

        return result;
        //
    }


    public async Task<decimal> GetTotalSales(CancellationToken cancellation, Expression<Func<OrderItem, bool>>? predicate = null, DateTime? start = null, DateTime? end = null)
    {
        if (start == null) start = DateTime.MinValue;
        if(end == null) end = DateTime.MaxValue;
        if (predicate == null)
            predicate = x => true;

       return await _entitySet
            .Where(predicate)
            .Where(x=>x.Order.OrderDate>=start && x.Order.OrderDate<=end)
            .SumAsync(x=>x.Quantity*x.ProductSupplier.Prices.Where(x=>x.ExpiredTime==null).Select(x=>x.PriceValue).FirstOrDefault(),cancellation);
    }



    public async Task<decimal> TotalDiscount(CancellationToken cancellation, Expression<Func<OrderItem, bool>>? predicate = null, DateTime? start = null, DateTime? end = null)
    {
        if (start == null) start = DateTime.MinValue;
        if (end == null) end = DateTime.MaxValue;
        if (predicate == null)
            predicate = x => true;

       return await _entitySet.Where(predicate).Where(x => x.Order.OrderDate >= start && x.Order.OrderDate <= end).SumAsync(x => x.UnitDiscount * x.Quantity,cancellation);
    }
}
