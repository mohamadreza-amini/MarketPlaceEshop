using DataTransferObject.DTOClasses.Order.Results;
using Model.Entities.Orders;
using Service.ServiceInterfaces.OrderServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses.OrderServices;

public class OrderService:ServiceBase<Order, OrderResult, Guid>,IOrderService
{
}
