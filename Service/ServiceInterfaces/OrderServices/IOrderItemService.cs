using DataTransferObject.DTOClasses.Order.Results;
using Model.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.OrderServices;

public interface IOrderItemService:IServiceBase<OrderItem,OrderItemResult,Guid>
{
    Task SendOrderItem(Guid orderItemId, CancellationToken cancellation);
    Task<List<ReportSalesResult>> GetDailySales(CancellationToken cancellation, int DaysCount = 7);
    Task<ReportSalesResult> GetTotalSales(CancellationToken cancellation, DateTime? start = null, DateTime? end = null);
}
