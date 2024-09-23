using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Inventory
{
    public Guid SellerId { get; set; }
    public Guid productId{ get; set; }

    public int quantity { get; set; }
    public Seller Seller { get; set; }
    public Product Product { get; set; }
}
