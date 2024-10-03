using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.Suppliers;

namespace Model.Entities.Orders;


public class CartItem : BaseEntity<Guid>
{
    public int Quantity { get; set; }
    public bool IsCanceled { get; set; }
    public Guid CartId { get; set; }
    public virtual Cart Cart { get; set; }
    public Guid ProductSupplierId { get; set; }
    public virtual ProductSupplier ProductSupplier { get; set; }

}
