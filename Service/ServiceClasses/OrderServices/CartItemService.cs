using DataTransferObject.DTOClasses.Order.Commands;
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
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses.OrderServices;

public class CartItemService : ServiceBase<CartItem, CartItemResult, Guid>, ICartItemService
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IProductSupplierService _productSupplierService;
    private readonly IUserService _userService;

    public CartItemService(ICartItemRepository cartItemRepository, IProductSupplierService productSupplierService, IUserService userService)
    {
        _cartItemRepository = cartItemRepository;
        _productSupplierService = productSupplierService;
        _userService = userService;
    }

    public async Task RefreshCartQuantitiesAsync(Guid productSupplierId, int quantity, CancellationToken cancellation)
    {
        await _cartItemRepository.UpdateQuantities(productSupplierId, quantity, cancellation);
    }

    public async Task<bool> IsItemInCartAsync(Guid customerId, Guid productSupplierId, CancellationToken cancellation)
    {
        return await _cartItemRepository.IsExistAsync(x => x.CustomerId == customerId && x.ProductSupplierId == productSupplierId, cancellation);
    }

    public async Task ChangeItemQuantityInCartAsync(CartItemCommand cartItemDto, CancellationToken cancellation)
    {
        var newCartItem = Translate<CartItemCommand, CartItem>(cartItemDto);
        newCartItem.Validate();

        var cartItem = await _cartItemRepository.GetAsync(x => x.ProductSupplierId == newCartItem.ProductSupplierId && x.CustomerId == newCartItem.CustomerId, cancellation);
        if (cartItem == null)
            throw new BadRequestException("آیتم سبد خرید پیدا نشد");
        cartItem.Quantity = newCartItem.Quantity;
        await _cartItemRepository.CommitAsync(cancellation);
    }

    public async Task AddCartItemAsync(CartItemCommand cartItemDto, CancellationToken cancellation)
    {
        var cartItem = Translate<CartItemCommand, CartItem>(cartItemDto);
        if (cartItem.CustomerId == Guid.Empty)
            throw new BadRequestException("ابتدا باید لاگین کنید");
        cartItem.Validate();
        var inventory = await _productSupplierService.GetInventory(cartItem.ProductSupplierId, cancellation);
        if (inventory > cartItem.Quantity)
            throw new BadRequestException("تعداد نامعتبر");
        if (await _productSupplierService.IsActiveProductSupplier(cartItem.ProductSupplierId, cancellation))
            throw new BadRequestException("محصول نامعتبر");

        var isItemInCart = await IsItemInCartAsync(cartItem.CustomerId, cartItem.ProductSupplierId, cancellation);
        if (isItemInCart)
        {
            await ChangeItemQuantityInCartAsync(cartItemDto, cancellation);
        }
        else
        {
            await _cartItemRepository.CreateAsync(cartItem, cancellation);
            await _cartItemRepository.CommitAsync(cancellation);
        }
    }

    public async Task DeleteCartItem(Guid customerId, Guid productSupplierId, CancellationToken cancellation)
    {
        if (customerId == Guid.Empty)
            throw new BadRequestException("ابتدا باید لاگین کنید");
        if (!_userService.IsAdmin() && _userService.RequesterId() != customerId.ToString())
            throw new AccessDeniedException();
        await _cartItemRepository.SoftDeleteAsync(x => x.CustomerId == customerId && x.ProductSupplierId == productSupplierId, cancellation);
        await _cartItemRepository.CommitAsync(cancellation);
    }



}
