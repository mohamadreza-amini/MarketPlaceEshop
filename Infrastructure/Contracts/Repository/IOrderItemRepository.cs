using Model.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contracts.Repository;

public interface IOrderItemRepository:IBaseRepository<OrderItem,Guid>
{
    Task<(List<decimal> totalSales, List<decimal> totalDiscount, List<DateTime> dateTimes)> GetDailySales(CancellationToken cancellation, Expression<Func<OrderItem, bool>> predicate = null, int DaysCount = 7);

    Task<decimal> TotalDiscount(CancellationToken cancellation, Expression<Func<OrderItem, bool>>? predicate = null, DateTime? start = null, DateTime? end = null);
    Task<decimal> GetTotalSales(CancellationToken cancellation, Expression<Func<OrderItem, bool>>? predicate = null, DateTime? start = null, DateTime? end = null);
}
