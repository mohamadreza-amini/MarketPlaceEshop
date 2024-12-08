using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Order.Results;

public class OrderItemResult : BaseDTO<Guid>
{
    public decimal UnitCost { get; set; }
    public decimal UnitDiscount { get; set; }
    public int Quantity { get; set; }
    public bool Sent { get; set; }
    public bool IsCanceled { get; set; }
    public DateTime? DateOfPosting { get; set; }
    public Guid ProductSupplierId { get; set; }
    public Guid OrderId { get; set; }
    public string ImagePath { get; set; }
    public string CompanyName { get; set; }
    public string ProductName { get; set; }
    public Guid ProductId { get; set; }

    // امکانات بیشتری اورده شد مثل عکس محصول  

}
