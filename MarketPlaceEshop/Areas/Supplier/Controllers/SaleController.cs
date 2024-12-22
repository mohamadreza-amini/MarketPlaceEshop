using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces.OrderServices;
using Shared.Enums;

namespace MarketPlaceEshop.Areas.Supplier.Controllers;
[Area("Supplier")]

public class SaleController : Controller
{
    private readonly IOrderService _orderService;

    public SaleController(IOrderService orderService)
    {
        _orderService = orderService;
    }

 
    public async Task<IActionResult> SendOrder(CancellationToken cancellation, int pageIndex = 1)
    {
        var orders = await _orderService.GetAllOrders(cancellation, pageIndex, 10,false, confirmationStatus: ConfirmationStatus.Confirmed);
        return View(orders);
    }


    public async Task OrderSent(Guid orderId, CancellationToken cancellation)
    {
        await _orderService.SendOrder(orderId, cancellation);
    }

  
}
