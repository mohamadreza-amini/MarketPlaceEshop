using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces.OrderServices;
using Shared.Enums;

namespace MarketPlaceEshop.Areas.Admin.Controllers;
[Area("Admin")]
public class SaleController : Controller
{
    private readonly IOrderService _orderService;

    public SaleController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> CheckOrder(CancellationToken cancellation, int pageIndex = 1)    
    {
        var orders = await _orderService.GetAllOrders(cancellation, pageIndex,10, confirmationStatus: ConfirmationStatus.Unchecked);
        return View(orders);
    }


    public async Task ConfirmOrder(Guid orderId, CancellationToken cancellation)
    {
        await _orderService.ConfitrmOrder(orderId,ConfirmationStatus.Confirmed,cancellation);
    }

    public async Task RejectOrder(Guid orderId, CancellationToken cancellation)
    {
        await _orderService.ConfitrmOrder(orderId, ConfirmationStatus.Rejected, cancellation);
    }
}
