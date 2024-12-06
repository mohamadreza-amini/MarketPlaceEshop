using DataTransferObject.DTOClasses.Order.Commands;
using DataTransferObject.DTOClasses.Order.Results;
using Infrastructure.Contracts.Repository;
using Model.Entities.Orders;
using Model.Entities.Person;
using Model.Exceptions;
using Service.ServiceInterfaces.OrderServices;
using Service.ServiceInterfaces.PersonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.ServiceClasses.OrderServices;

public class OrderService : ServiceBase<Order, OrderResult, Guid>, IOrderService
{
    private readonly IBaseRepository<Order, Guid> _orderRepository;
    private readonly IUserService _userService;
    private readonly ICartItemService _cartItemService;

    public OrderService(IBaseRepository<Order, Guid> orderRepository, IUserService userService, ICartItemService cartItemService)
    {
        _orderRepository = orderRepository;
        _userService = userService;
        _cartItemService = cartItemService;
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




}
