using DataTransferObject.DTOClasses.Order.Commands;
using DataTransferObject.DTOClasses.Order.Results;
using Infrastructure.Contracts.Repository;
using Microsoft.AspNetCore.Mvc;
using Model.Exceptions;
using Shared;
using Service.ServiceInterfaces.AddressServices;
using Service.ServiceInterfaces.OrderServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MarketPlaceEshop.Controllers;

[Authorize(Roles = "Customer")]
public class CartController : Controller
{
    private readonly ICartItemService _cartItemService;
    private readonly IOrderService _orderService;
    private readonly IAdressService _adressService;

    public CartController(ICartItemService cartItemService, IOrderService orderService, IAdressService adressService)
    {
        _cartItemService = cartItemService;
        _orderService = orderService;
        _adressService = adressService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<string> AddToCart(CartItemCommand CartItemDto, CancellationToken cancellation)
    {
        CartItemDto.CustomerId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new AccessDeniedException());
        try
        {
            await _cartItemService.AddCartItemAsync(CartItemDto, cancellation);
            return "به سبد خرید اضافه شد";
        }
        catch (BaseException ex)
        {
            return ex.Message;
        }
    }

    public async Task<string> DeleteCart(Guid productSupplierId, CancellationToken cancellation)
    {
        var customerId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new AccessDeniedException());
        try
        {
            await _cartItemService.DeleteCartItem(customerId, productSupplierId, cancellation);
            return "از سبد خرید حذف شد";
        }
        catch (BaseException ex)
        {
            return ex.Message;
        }
    }

    public async Task<IActionResult> GetCart(CancellationToken cancellation)
    {
        var carts = await _cartItemService.GetCartByCustomerId(cancellation);
        return View(carts);
    }


    [HttpGet]
    public async Task<IActionResult> AddOrder(CancellationToken cancellation)
    {
        Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new AccessDeniedException(), out Guid customerId);
        var address = await _adressService.GetAllByCustomerIdAsync(customerId, cancellation);
        if (address == null || !address.Any())
            return RedirectToAction("address", "Account");
        var cart = await _cartItemService.GetCartByCustomerId(cancellation);

        return View((address, cart));
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder(OrderCommand orderDto, CancellationToken cancellation)
    {
        orderDto.CustomerId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new AccessDeniedException());
        try
        {
            await _orderService.AddOrderAsync(orderDto, cancellation);
        }
        catch (BadRequestException ex)
        {
            ViewData["failed"] = ex.Message.Replace("Bad request!","");
        }
        ViewData["shippeddate"] = orderDto.ShippedDate.ToAD().ToString("yyyy/MM/dd");
        return View("ShoppingComplete");
    }
}
