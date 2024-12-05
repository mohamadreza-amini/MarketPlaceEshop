﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Order.Commands;

public class OrderItemCommand
{
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal UnitDiscount { get; set; }
    public Guid ProductSupplierId { get; set; }
    public Guid OrderId { get; set; }
}
