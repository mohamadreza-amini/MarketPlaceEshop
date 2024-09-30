using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class OrderItem : BaseEntity<Guid>
{
    public decimal Cost {  get; set; }
    public decimal UnitCost {  get; set; }
    public decimal Discount { get; set; }
    public decimal UnitDiscount {  get; set; }
    public int Quantity { get; set; }
    public bool Sent { get; set; }
    public bool IsCanceled { get; set; }
    public DateTime DateOfPosting { get; set; }
    public Guid ProductSupplierId { get; set; }
    public ProductSupplier ProductSupplier { get; set; }
    public Guid OrderId {  get; set; }
    public Order Order { get; set; }
    

}
