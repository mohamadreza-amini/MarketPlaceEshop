using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class ProductSupplier : BaseEntity<Guid>
{
    //دیفالت صفر و فقط مثبت
    public int Ventory { get; set; }
    //nullable riali
    // بررسی nullable اصلا باید بزاری یا نه
    public Decimal Discount { get; set; }
    public bool IsDisable { get; set; }
    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Score> Scores { get; set; }
    public ICollection<Price> Prices { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
    //
}
