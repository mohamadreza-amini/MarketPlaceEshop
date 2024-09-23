using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class OrderItem
{
    public Guid OrderId {  get; set; }
    public Guid OrderItemId {  get; set; }
    public Order order { get; set; }
    public Guid ProductId { get; set; }
    public Product product { get; set; }
    public int quantity {  get; set; }
    public decimal price { get; set; }
    public decimal Discount { get; set; }

}
