using Model.Entities.Addresses;
using Model.Entities.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Orders;

public class Order : BaseEntity<Guid>
{
    public DateTime OrderDate { get; set; }
    public DateTime ShippedDate { get; set; }
    public Guid AddressId { get; set; }
    public virtual Address Address { get; set; }
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }

}
