using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces.OrderServices;

namespace MarketPlaceEshop.ViewComponents;

public class CartTabViewComponent : ViewComponent
{
    private readonly ICartItemService _cartItemService;

    public CartTabViewComponent(ICartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellation)
    {
        var carts = await _cartItemService.GetCartByCustomerId(cancellation);
        return View(carts);
    }
}