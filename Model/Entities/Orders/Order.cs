using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.Customers;

namespace Model.Entities.Orders;

public class Order : BaseEntity<Guid>
{
    public decimal TotalCost { get; set; }
    public decimal TotalDiscount { get; set; }
    public bool IsCanceled { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShippedDate { get; set; }
    public Guid AddressId { get; set; }
    public virtual Address Address { get; set; }
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }

}
