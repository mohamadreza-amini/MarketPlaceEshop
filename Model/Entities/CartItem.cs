using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class CartItem : BaseEntity<Guid>
{
    public int Quantity { get; set; }
    public bool IsCanceled { get; set; }
    public Guid CartId { get; set; }
    public Cart Cart { get; set; }
    public Guid ProductSupplierId { get; set; }
    public ProductSupplier ProductSupplier { get; set; }

}
