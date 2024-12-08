using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Order.Results;

public class CartResult
{
    public List<CartItemResult> CartItems { get; set; }
    public decimal TotalAmountPaid { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalPrice { get; set; }
}
