using DataTransferObject.DTOClasses.Order.Commands;
using Infrastructure.Contracts.Repository;
using Microsoft.AspNetCore.Mvc;
using Model.Exceptions;
using Service.ServiceInterfaces.OrderServices;
using System.Security.Claims;

namespace MarketPlaceEshop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartItemService _cartItemService;

        public CartController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
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
            catch (BaseException ex) {
                return ex.Message;
            }

        }
    }
}
