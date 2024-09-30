using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Price : BaseEntity<Guid>
{
    public decimal PriceValue {  get; set; }
    public DateTime StartTime { get; set; }
    //پایینی nullable
    public DateTime ExpiredTime { get; set; }
    public Guid ProductSupplierId { get; set; }
    public ProductSupplier productSupplier { get; set; }
    //

}
