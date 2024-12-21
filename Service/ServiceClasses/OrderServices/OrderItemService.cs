using DataTransferObject.DTOClasses.Order.Results;
using Infrastructure.Contracts.Repository;
using Model.Entities.Orders;
using Model.Exceptions;
using Service.ServiceInterfaces.OrderServices;
using Service.ServiceInterfaces.PersonServices;
using Service.ServiceInterfaces.ProductServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses.OrderServices;

public class OrderItemService : ServiceBase<OrderItem, OrderItemResult, Guid>, IOrderItemService
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IUserService _userService;
    private readonly IProductSupplierService _productSupplierService;
    public OrderItemService(IOrderItemRepository orderItemRepository, IUserService userService, IProductSupplierService productSupplierService)
    {
        _orderItemRepository = orderItemRepository;
        _userService = userService;
        _productSupplierService = productSupplierService;
    }

    public async Task SendOrderItem(Guid orderItemId, CancellationToken cancellation)
    {
        var orderItem = await _orderItemRepository.GetByIdAsync(orderItemId, cancellation);
        if (orderItem == null)
            throw new BadRequestException();
        var supplierId = await _productSupplierService.GetSuppierIdById(orderItem.ProductSupplierId, cancellation);
        Guid.TryParse(_userService.RequesterId(), out Guid requesterId);

        if (!_userService.IsAdmin() && requesterId != supplierId)
            throw new AccessDeniedException();
        orderItem.Sent = true;
        orderItem.DateOfPosting = DateTime.Now;
        orderItem.UpdaterUserId = requesterId;
        await _orderItemRepository.CommitAsync(cancellation);
    }

    //گزارش فروش تخفیف پرداخت روزانه بر اساس تعداد روز اگه ادمین بود همه اگه تامین کننده بود مال خودش
    public async Task<List<ReportSalesResult>> GetDailySales(CancellationToken cancellation, int DaysCount = 7)
    {
        Expression<Func<OrderItem, bool>>? predicate = null;
        predicate = InitializationPredicate(predicate);

        var result = await _orderItemRepository.GetDailySales(cancellation, predicate, DaysCount);

        return result.dateTimes
        .Select((date, index) => new ReportSalesResult
        {
            DateTime = date,
            TotalSales = result.totalSales[index],
            TotalDiscount = result.totalDiscount[index]
        })
        .ToList();

        //
    }



    //مجموع فروش تخفیف پرداخت کل اگه بازه بدیم توی همون بازه وگرنه کل براساس شخص اگه ادمین باشه همه وگرنه همون تامین کننده
    public async Task<ReportSalesResult> GetTotalSales(CancellationToken cancellation, DateTime? start = null, DateTime? end = null)
    {
        Expression<Func<OrderItem, bool>>? predicate = null;
        predicate = InitializationPredicate(predicate);

        var result = new ReportSalesResult();
        result.TotalDiscount = await _orderItemRepository.TotalDiscount(cancellation, predicate, start, end);
        result.TotalSales = await _orderItemRepository.GetTotalSales(cancellation, predicate, start, end);
        result.TotalAmountPaid = result.TotalSales - result.TotalDiscount;

        return result;
    }



    private Expression<Func<OrderItem, bool>>? InitializationPredicate(Expression<Func<OrderItem, bool>>? predicate)
    {
        if (_userService.IsAdmin())
        {
            predicate = x => true;
        }
        else if (_userService.IsInRole("Supplier") && Guid.TryParse(_userService.RequesterId(), out Guid supplierId))
        {
            predicate = x => x.ProductSupplier.SupplierId == supplierId;
        }
        else
        {
            throw new AccessDeniedException();
        }
        return predicate;
    }



}
