using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.Orders;
using Model.Entities.Products;
using Model.Entities.Review;

namespace Model.Entities.Suppliers;

public class ProductSupplier : BaseEntity<Guid>
{
    public int Ventory { get; set; }
    public decimal Discount { get; set; }
    public bool IsDisable { get; set; }
    public Guid SupplierId { get; set; }
    public virtual Supplier Supplier { get; set; }
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
    public virtual ICollection<Comment>? Comments { get; set; }
    public virtual ICollection<Score>? Scores { get; set; }
    public virtual ICollection<Price> Prices { get; set; }
    public virtual ICollection<OrderItem>? OrderItems { get; set; }
    public virtual ICollection<CartItem>? CartItems { get; set; }
}
