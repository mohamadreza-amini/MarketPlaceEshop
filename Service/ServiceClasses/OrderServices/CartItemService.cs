﻿using DataTransferObject.DTOClasses.Order.Commands;
using DataTransferObject.DTOClasses.Order.Results;
using Infrastructure.Contracts.Repository;
using Microsoft.EntityFrameworkCore;
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
    //رفرش کردن ظرفیت تمام سبد خرید های حامل یک محصول خاص
    public async Task RefreshCartQuantitiesByIdAsync(Guid productSupplierId, int quantity, CancellationToken cancellation)
    {
        await _cartItemRepository.UpdateQuantitiesById(productSupplierId, quantity, cancellation);
    }

    //رفرش کردن ظرفیت تمام سبد خرید های مشکل دار
    public async Task RefreshAllCartQuantitiesAsync(CancellationToken cancellation)
    {
        await _cartItemRepository.UpdateAllQuantities(cancellation);
    }

    //بررسی بودن یک ایتم در سبد خرید
    public async Task<bool> IsItemInCartAsync(Guid customerId, Guid productSupplierId, CancellationToken cancellation)
    {
        return await _cartItemRepository.IsExistAsync(x => x.CustomerId == customerId && x.ProductSupplierId == productSupplierId, cancellation);
    }
    //تغیر تعداد یک ایتم سبد خرید
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
    //اضافه ایتمی به سبد خرید
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
    //حدف ایتمی از سبد خرید
    public async Task DeleteCartItem(Guid customerId, Guid productSupplierId, CancellationToken cancellation)
    {
        if (customerId == Guid.Empty)
            throw new BadRequestException("ابتدا باید لاگین کنید");
        if (!_userService.IsAdmin() && _userService.RequesterId() != customerId.ToString())
            throw new AccessDeniedException();
        await _cartItemRepository.SoftDeleteAsync(x => x.CustomerId == customerId && x.ProductSupplierId == productSupplierId, cancellation);
        await _cartItemRepository.CommitAsync(cancellation);
    }
    //ریست سبد خرید بعد خرید
    public async Task<bool> ResetCartItem(Guid customerId, CancellationToken cancellation)
    {
        return await _cartItemRepository.DeleteAllByCustomerId(customerId, cancellation) > 0;
    }

    //آماده کردن ابجکتی برای انتقال از سبد خرید به اردرایتم
    public async Task<List<OrderItemCommand>> TransferCartToOrderItems(Guid customerId, CancellationToken cancellation)
    {
        var query = await _cartItemRepository.GetAllDataAsync(x => new OrderItemCommand
        {
            ProductSupplierId = x.ProductSupplierId,
            Quantity = x.Quantity,
            UnitDiscount = x.ProductSupplier.Discount,
            UnitCost = x.ProductSupplier.Prices.Where(x => x.ExpiredTime == null).Select(x => x.PriceValue).FirstOrDefault(),
        }, cancellation, x => x.CustomerId == customerId && x.ProductSupplier.Prices.Any(x => x.ExpiredTime == null), x => x.Include(x => x.ProductSupplier).ThenInclude(x => x.Prices));

        if (query == null)
            throw new BadRequestException();

        return await query.ToListAsync(cancellation);
    }


    //برسی موجود بودن ظرفیت تمام اعضای سبد خرید
    public async Task<bool> IsAvailableCartItems(Guid customerId, CancellationToken cancellation)
    {
        var query = await _cartItemRepository.GetAllDataAsync(x => x, cancellation, x => x.CustomerId == customerId, x => x.Include(x => x.ProductSupplier));
        if (query == null)
            return true;

        var cartitems = await query.ToListAsync(cancellation);
        var result = true;
        foreach (var cartitem in cartitems)
        {
            if (cartitem.Quantity != cartitem.ProductSupplier.Ventory)
            {
                await RefreshCartQuantitiesByIdAsync(cartitem.ProductSupplierId, cartitem.ProductSupplier.Ventory, cancellation);
                result = false;
            }
        }
        return result;
    }


    //کاهش موجودی بعد خرید
    public async Task<bool> DecreaseInventoryOnSale(Guid customerId, CancellationToken cancellation)
    {
        var query = await _cartItemRepository.GetAllDataAsync(x => x, cancellation, x => x.CustomerId == customerId, x => x.Include(x => x.ProductSupplier).ThenInclude(x => x.Prices), false);
        if (query == null)
            return false;
        var cartitems = await query.ToListAsync(cancellation);
        foreach (var cartitem in cartitems)
        {
            cartitem.ProductSupplier.Ventory -= cartitem.Quantity;
            if (cartitem.ProductSupplier.Ventory == 0)
            {
                var expirePrice = cartitem.ProductSupplier.Prices.Where(x => x.ExpiredTime != null).FirstOrDefault();
                if (expirePrice != null) expirePrice.ExpiredTime = DateTime.Now;
            }
        }
        return true;
        //اگه بعد از پیاده سازی تغیراتش اعمال نشد کامیت رو اینجا هم اضافه کن
    }
}