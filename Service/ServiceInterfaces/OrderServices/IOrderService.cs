using DataTransferObject.DTOClasses.Order.Commands;
using DataTransferObject.DTOClasses.Order.Results;
using Model.Entities.Orders;
using Service.ServiceClasses;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.OrderServices;

public interface IOrderService : IServiceBase<Order, OrderResult, Guid>
{
    Task<bool> AddOrderAsync(OrderCommand orderDto, CancellationToken cancellationToken);
    Task<PaginatedList<OrderResult>> GetAllOrders(CancellationToken cancellation, int pageIndex = 1, int pageSize = 20, bool? sent = null, ConfirmationStatus? confirmationStatus = null);
    Task<PaginatedList<OrderItemResult>> GetAllOrderItems(CancellationToken cancellation, int pageIndex = 1, int pageSize = 20, bool? sent = null, ConfirmationStatus? confirmationStatus = null);
    Task ConfitrmOrder(Guid OrderId, ConfirmationStatus confirmation, CancellationToken cancellation);
    Task SendOrder(Guid orderId, CancellationToken cancellation);
    Task<int> NumberOfOrders(ConfirmationStatus confirmation, CancellationToken cancellation);
    Task<int> NumberOfOrders(bool sent, CancellationToken cancellation);

}
