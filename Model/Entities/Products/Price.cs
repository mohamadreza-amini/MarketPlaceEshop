using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.Suppliers;

namespace Model.Entities.Products;

public class Price : BaseEntity<Guid>
{
    public decimal PriceValue { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? ExpiredTime { get; set; }
    public Guid ProductSupplierId { get; set; }
    public virtual ProductSupplier ProductSupplier { get; set; }
}
