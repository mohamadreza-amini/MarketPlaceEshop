using DataTransferObject.DTOClasses.Order.Commands;
using DataTransferObject.DTOClasses.Order.Results;
using Infrastructure.Contracts.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Model.Entities.Orders;
using Model.Entities.Person;
using Model.Entities.Products;
using Model.Exceptions;
using Service.ServiceClasses.PersonServices;
using Service.ServiceInterfaces.OrderServices;
using Service.ServiceInterfaces.PersonServices;
using Service.ServiceInterfaces.ProductServices;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Service.ServiceClasses.OrderServices;

public class OrderService : ServiceBase<Order, OrderResult, Guid>, IOrderService
{
    private readonly IBaseRepository<Order, Guid> _orderRepository;
    private readonly IUserService _userService;
    private readonly ICartItemService _cartItemService;
    private readonly IProductSupplierService _productSupplierService;

    public OrderService(IBaseRepository<Order, Guid> orderRepository, IUserService userService, ICartItemService cartItemService, IProductSupplierService productSupplierService)
    {
        _orderRepository = orderRepository;
        _userService = userService;
        _cartItemService = cartItemService;
        _productSupplierService = productSupplierService;
    }


    public async Task<bool> AddOrderAsync(OrderCommand orderDto, CancellationToken cancellationToken)
    {
        if (orderDto.CustomerId == Guid.Empty)
            throw new BadRequestException("ابتدا باید لاگین کنید");
        if (!_userService.IsAdmin() && _userService.RequesterId() != orderDto.CustomerId.ToString())
            throw new AccessDeniedException();

        var order = await CreateOrder(orderDto, cancellationToken);

        bool IsAvailableCartItems = await _cartItemService.IsAvailableCartItems(order.CustomerId, cancellationToken);
        if (!IsAvailableCartItems)
            throw new BadRequestException("بعضی اقلام موجود نیست");

        var isDecreaseInventory = await _cartItemService.DecreaseInventoryOnSale(order.CustomerId, cancellationToken);
        if (!isDecreaseInventory)
            throw new BadRequestException("خطا در عملیات");
        try
        {
            await _orderRepository.CreateAsync(order, cancellationToken);
            await _orderRepository.CommitAsync(cancellationToken);
            await _cartItemService.ResetCartItem(order.CustomerId, cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            //اینجا میشه لاگ زد
            return false;
        }
        finally
        {
            await _cartItemService.RefreshAllCartQuantitiesAsync(cancellationToken);
        }
    }



    private async Task<Order> CreateOrder(OrderCommand orderDto, CancellationToken cancellationToken)
    {
        var order = Translate<OrderCommand, Order>(orderDto);
        order.OrderDate = DateTime.Now;
        order.CreatorUserId = Guid.Parse(_userService.RequesterId() ?? Guid.Empty.ToString());
        order.UpdaterUserId = order.CreatorUserId;
        order.IsConfirmed = 1;

        var orderitems = await _cartItemService.TransferCartToOrderItems(orderDto.CustomerId, cancellationToken);
        order.OrderItems = Translate<List<OrderItemCommand>, List<OrderItem>>(orderitems);
        Guid.TryParse(_userService.RequesterId(), out Guid requesterId);
        foreach (var orderItem in order.OrderItems)
        {
            orderItem.CreatorUserId = orderItem.UpdaterUserId = requesterId;
        }
        order.Validate();
        return order;
    }



    //دیدن لیست اردرها براساس شخص یعنی کاربر باشه برای خودش ادمین باشه همه رو تامین کننده برای خودش به همراه لیست اردرایتم ها  با قابلیت فیلتر ارسال شده نشده تایید شده نشده 
    public async Task<PaginatedList<OrderResult>> GetAllOrders(CancellationToken cancellation, int pageIndex = 1, int pageSize = 20, bool? sent = null, ConfirmationStatus? confirmationStatus = null)
    {
        var query = await _orderRepository.GetAllDataAsync(x => x, cancellation);
        if (query == null)
            return new PaginatedList<OrderResult>(new List<OrderResult> { new OrderResult() { OrderItems = new List<OrderItemResult>() } }, 0, 1, pageSize) { };

        Expression<Func<OrderItem, bool>> sentFilter = x => true;
        query = await FilterByPerson(query);
        query = SetFilters(query, sentFilter, sent, confirmationStatus);
        var result = SelectOrders(query, sentFilter);
        return await PaginatedList<OrderResult>.CreateAsync(result, pageIndex, pageSize, cancellation);
    }

    //همون بالایی ولی فقط اردر ایتم هارو میده
    public async Task<PaginatedList<OrderItemResult>> GetAllOrderItems(CancellationToken cancellation, int pageIndex = 1, int pageSize = 20, bool? sent = null, ConfirmationStatus? confirmationStatus = null)
    {
        var query = await _orderRepository.GetAllDataAsync(x => x, cancellation);
        if (query == null)
            return new PaginatedList<OrderItemResult>(new List<OrderItemResult>(), 0, 1, pageSize) { };

        Expression<Func<OrderItem, bool>> sentFilter = x => true;
        query = await FilterByPerson(query);

        query = SetFilters(query, sentFilter, sent, confirmationStatus);
        var result = GetOrderItems(query, sent);
        return await PaginatedList<OrderItemResult>.CreateAsync(result, pageIndex, pageSize, cancellation);
    }



    private IQueryable<Order> SetFilters(IQueryable<Order> query, Expression<Func<OrderItem, bool>> sentFilter, bool? sent = null, ConfirmationStatus? confirmationStatus = null)
    {
        if (confirmationStatus != null)
        {
            query = query.Where(x => x.IsConfirmed == (byte)confirmationStatus.Value);
        }
        if (sent != null)
        {
            query = query.Where(x => x.OrderItems.Any(x => x.Sent == sent));
            sentFilter = x => x.Sent == sent;
        }
        return query;
    }


    private async Task<IQueryable<Order>> FilterByPerson(IQueryable<Order> query)
    {
        if (!Guid.TryParse(_userService.RequesterId(), out Guid requesterId))
            throw new AccessDeniedException();

        switch (await _userService.GetRole())
        {
            case ("Admin"):
                break;

            case ("Supplier"):
                query = query.Where(x => x.OrderItems.Any(x => x.ProductSupplier.SupplierId == requesterId));
                break;

            case ("Customer"):
                query = query.Where(x => x.CustomerId == requesterId);
                break;

            default:
                throw new AccessDeniedException();
        }
        return query;
    }



    private static IQueryable<OrderResult> SelectOrders(IQueryable<Order> query, Expression<Func<OrderItem, bool>> sentFilter)
    {
        return query.Select(x => new OrderResult
        {
            Id = x.Id,
            AddressId = x.AddressId,
            CustomerId = x.CustomerId,
            IsConfirm = (ConfirmationStatus)x.IsConfirmed,
            ShippedDate = x.ShippedDate,
            OrderDate = x.OrderDate,
            TotalPrice = x.OrderItems.Sum(x => x.Quantity * x.UnitCost),
            TotalDiscount = x.OrderItems.Sum(x => x.Quantity * x.UnitDiscount),
            TotalAmountPaid = x.OrderItems.Sum(x => x.Quantity * x.UnitCost) - x.OrderItems.Sum(x => x.Quantity * x.UnitDiscount),
            FullAddress = x.Address.City.Province.ProvinceName + "-" + x.Address.City.CityName + "-" + x.Address.Neighborhood + "," + x.Address.AddressDetail + " پلاک," + x.Address.HouseNumber + " واحد," + x.Address.UnitNumber + " کدپستی," + x.Address.PostalCode,
            OrderItems = x.OrderItems.AsQueryable().Where(sentFilter).Select(x => new OrderItemResult
            {
                Id = x.Id,
                DateOfPosting = x.DateOfPosting,
                OrderId = x.OrderId,
                ProductId = x.ProductSupplier.ProductId,
                Sent = x.Sent,
                UnitCost = x.UnitCost,
                UnitDiscount = x.UnitDiscount,
                ImagePath = x.ProductSupplier.Product.Images.Select(x => x.Path).FirstOrDefault()!,
                Quantity = x.Quantity,
                IsCanceled = x.IsCanceled,
                ProductName = x.ProductSupplier.Product.Name,
                ProductSupplierId = x.ProductSupplier.Id,
                CompanyName = x.ProductSupplier.Supplier.CompanyName,
            }).ToList()
        });
    }

     

    private static IQueryable<OrderItemResult> GetOrderItems(IQueryable<Order> orders, bool? sent = null)
    {
        Expression<Func<OrderItem, bool>> sentFilter = x => true;
        if (sent != null)
            sentFilter = x => x.Sent == sent;

        return orders.SelectMany(x => x.OrderItems).Where(sentFilter).Select(x => new OrderItemResult
        {
            Id = x.Id,
            DateOfPosting = x.DateOfPosting,
            OrderId = x.OrderId,
            ProductId = x.ProductSupplier.ProductId,
            Sent = x.Sent,
            UnitCost = x.UnitCost,
            UnitDiscount = x.UnitDiscount,
            ImagePath = x.ProductSupplier.Product.Images.Select(x => x.Path).FirstOrDefault()!,
            Quantity = x.Quantity,
            IsCanceled = x.IsCanceled,
            ProductName = x.ProductSupplier.Product.Name,
            ProductSupplierId = x.ProductSupplier.Id,
            CompanyName = x.ProductSupplier.Supplier.CompanyName,
        });
    }


    public async Task ConfitrmOrder(Guid OrderId, ConfirmationStatus confirmation, CancellationToken cancellation)
    {
        if (!_userService.IsAdmin())
            throw new AccessDeniedException();
        Guid.TryParse(_userService.RequesterId(), out Guid adminId);

        var order = await _orderRepository.GetByIdAsync(OrderId, cancellation);
        if (order == null)
            throw new BadRequestException("سفارش یافت نشد");

        order.IsConfirmed = (byte)confirmation;
        order.UpdaterUserId = adminId;
        if (confirmation == ConfirmationStatus.Confirmed)
        {
            order.ConfirmedDate = DateTime.UtcNow;
            order.AdminConfirmedId = adminId;
        }
        await _orderRepository.CommitAsync(cancellation);
    }


    //براساس ایننکه ادمین یا تامین کننده فراخوانی کنه میره اردرایتم های اردر را ارسال میکنه

    public async Task SendOrder(Guid orderId, CancellationToken cancellation)
    {
        if (!_userService.IsAdmin() && _userService.IsInRole("Supplier"))
            throw new AccessDeniedException();
        var query = await _orderRepository.GetAllDataAsync(x => x, cancellation, x => x.Id == orderId, x => x.Include(x => x.OrderItems).ThenInclude(x => x.ProductSupplier), false);
        if (query == null)
            throw new BadRequestException("سفارش پیدا نشد");

        var order = await query.FirstOrDefaultAsync(cancellation);
        UpdateSentOrder(order!);
        await _orderRepository.CommitAsync(cancellation);
    }

    private void UpdateSentOrder(Order order)
    {
        Guid.TryParse(_userService.RequesterId(), out Guid requesterId);

        if (_userService.IsAdmin())
        {
            order!.OrderItems.ToList().ForEach(x =>
            {
                x.Sent = true;
                x.DateOfPosting = DateTime.Now;
                x.UpdaterUserId = requesterId;
            });
        }
        else
        {
            order!.OrderItems.Where(x => x.ProductSupplier.SupplierId == requesterId).ToList().ForEach(x =>
            {
                x.Sent = true;
                x.DateOfPosting = DateTime.Now;
                x.UpdaterUserId = requesterId;
            });
        }
    }



}



























