using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.Products;

namespace Model.Entities.Orders;

public class OrderItem : BaseEntity<Guid>
{
    public decimal UnitCost { get; set; }
    public decimal UnitDiscount { get; set; }
    public int Quantity { get; set; }
    public bool Sent { get; set; }
    public bool IsCanceled { get; set; }
    public DateTime? DateOfPosting { get; set; }
    public Guid ProductSupplierId { get; set; }
    public virtual ProductSupplier ProductSupplier { get; set; }
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }

}
