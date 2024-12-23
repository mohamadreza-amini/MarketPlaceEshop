using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Order.Results;

public class CartItemResult:BaseDTO<Guid>
{
    public int Quantity { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ProductSupplierId { get; set; }
    public string ImagePath { get; set; }
    public int Ventory { get; set; }
    public decimal Discount { get; set; }
    public decimal PriceValue { get; set; }
    public string CompanyName { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
}
