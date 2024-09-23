using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Order
{
    public Guid OrderId { get; set; }
    public Guid CustomerId {  get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public decimal TotalCost { get; set; }
    public decimal TotalDiscount { get; set; }
    public ICollection<OrderItem> Items { get; set; }
    public Guid PaymentId { get; set; }
    public Payment Payment { get; set; }

}
