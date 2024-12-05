using DataTransferObject.DTOClasses.Order.Results;
using Model.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.OrderServices;

public interface IOrderItemService:IServiceBase<OrderItem,OrderItemResult,Guid>
{
}
