using Model.Entities.Addresses;
using Model.Entities.Categories;
using Model.Entities.Person;
using Model.Entities.Products;
using Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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




    public void Validate()
    {
        if (AddressId==Guid.Empty||
            CustomerId==Guid.Empty||
            OrderDate.Equals(default)||
            ShippedDate.Equals(default))
        {
            throw new ModelValidationException("Order");
        }
    }
}
