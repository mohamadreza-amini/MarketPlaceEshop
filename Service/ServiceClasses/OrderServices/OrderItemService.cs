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

public class OrderItemService : ServiceBase<OrderItem, OrderItemResult, Guid>, IOrderItemService
{
    private readonly IBaseRepository<OrderItem, Guid> _orderItemRepository;
    private readonly IUserService _userService;
    private readonly IProductSupplierService _productSupplierService;
    public OrderItemService(IBaseRepository<OrderItem, Guid> orderItemRepository, IUserService userService, IProductSupplierService productSupplierService)
    {
        _orderItemRepository = orderItemRepository;
        _userService = userService;
        _productSupplierService = productSupplierService;
    }

    public async Task SendOrderItem(Guid orderItemId, CancellationToken cancellation)
    {
        var orderItem = await _orderItemRepository.GetByIdAsync(orderItemId, cancellation);
        if (orderItem == null)
            throw new BadRequestException();
        var supplierId = await _productSupplierService.GetSuppierIdById(orderItem.ProductSupplierId, cancellation);
        Guid.TryParse(_userService.RequesterId(), out Guid requesterId);

        if (!_userService.IsAdmin() && requesterId != supplierId)
            throw new AccessDeniedException();
        orderItem.Sent = true;
        orderItem.DateOfPosting = DateTime.Now;
        orderItem.UpdaterUserId = requesterId;
        await _orderItemRepository.CommitAsync(cancellation);
    }
}
