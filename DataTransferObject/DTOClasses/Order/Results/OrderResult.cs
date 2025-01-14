﻿using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Order.Results;

public class OrderResult : BaseDTO<Guid>
{
    public DateTime OrderDate { get; set; }
    public DateTime ShippedDate { get; set; }
    public Guid AddressId { get; set; }
    public string FullAddress { get; set; }
    public Guid CustomerId { get; set; }
    public List<OrderItemResult> OrderItems { get; set; }
    public ConfirmationStatus IsConfirm { get; set; }
    public decimal TotalAmountPaid { get; set; }
    public decimal TotalDiscount {  get; set; }
    public decimal TotalPrice { get; set; }
}
