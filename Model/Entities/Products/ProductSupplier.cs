using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.Orders;
using Model.Entities.Person;
using Model.Entities.Review;
using Model.Exceptions;

namespace Model.Entities.Products;

public class ProductSupplier : BaseEntity<Guid>
{
    public int Ventory { get; set; }
    public decimal Discount { get; set; }
    public bool IsDisable { get; set; }
    public Guid SupplierId { get; set; }
    public virtual Supplier Supplier { get; set; }
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
    public virtual ICollection<Price> Prices { get; set; }
    public virtual ICollection<OrderItem>? OrderItems { get; set; }
    public virtual ICollection<CartItem>? CartItems { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }

    public void Validate()
    {
        if (Ventory < 0 ||
            Discount < 0 ||
            SupplierId == Guid.Empty ||
            ProductId == Guid.Empty)
        {
            throw new ModelValidationException("ProductSupplier");
        }
    }
}
