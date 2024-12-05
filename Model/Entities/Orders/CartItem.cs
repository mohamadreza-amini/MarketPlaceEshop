using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.Categories;
using System.Xml.Linq;
using Model.Entities.Person;
using Model.Entities.Products;
using Model.Exceptions;

namespace Model.Entities.Orders;


public class CartItem : BaseEntity<Guid>
{
    public int Quantity { get; set; }
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public Guid ProductSupplierId { get; set; }
    public virtual ProductSupplier ProductSupplier { get; set; }


    public void Validate()
    {
        if (Quantity <= 0 ||
            CustomerId == Guid.Empty ||
            ProductSupplierId == Guid.Empty)
        {
            throw new ModelValidationException("CartItem");
        }
    }
}
