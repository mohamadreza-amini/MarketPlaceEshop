using MarketPlaceEshop.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces.OrderServices;
using Service.ServiceInterfaces.PersonServices;
using Service.ServiceInterfaces.ProductServices;
using Service.ServiceInterfaces.ReportingServices;
using Shared;
using Shared.Enums;
using System.Composition;

namespace MarketPlaceEshop.Areas.Supplier.Controllers;

[Area("Supplier")]
[Authorize(Roles = "Supplier")]
public class ReportController : Controller
{
    private readonly ICartItemService _cartItemService;
    private readonly IOrderItemService _orderItemService;
    private readonly IOrderService _orderService;
    private readonly ICustomerService _customerService;
    private readonly ISupplierService _supplierService;
    private readonly IProductSupplierService _productSupplierService;
    private readonly IViewLogService _viewLogService;
    public ReportController(ICartItemService cartItemService, IOrderItemService orderItemService, IOrderService orderService, ICustomerService customerService, ISupplierService supplierService, IProductSupplierService productSupplierService, IViewLogService viewLogService)
    {
        _cartItemService = cartItemService;
        _orderItemService = orderItemService;
        _orderService = orderService;
        _customerService = customerService;
        _supplierService = supplierService;
        _productSupplierService = productSupplierService;
        _viewLogService = viewLogService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellation, ReportViewModel report)
    {
        var reports = new ReportViewModel();

        reports.DailySales = await _orderItemService.GetDailySales(cancellation, report.saleDays);
        reports.TotalSales = await _orderItemService.GetTotalSales(cancellation, report.startOfSalePeriod.ToSolar(), report.endOfSalePeriod.ToSolar());

        reports.NumberOfOrderConfirmed = await _orderService.NumberOfOrders(ConfirmationStatus.Confirmed, cancellation);
        reports.NumberOfOrderRejected = await _orderService.NumberOfOrders(ConfirmationStatus.Rejected, cancellation);
        reports.NumberOfOrderUnchecked = await _orderService.NumberOfOrders(ConfirmationStatus.Unchecked, cancellation);
        reports.NumberOfOrderSending = await _orderService.NumberOfOrders(false, cancellation);
        reports.NumberOfOrderSent = await _orderService.NumberOfOrders(true, cancellation);

        reports.TotalInventory = await _productSupplierService.GetTotalInventory(cancellation);

        return View("Reports",reports);
    }
}
