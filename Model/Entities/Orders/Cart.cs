using Model.Entities.Customers;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Orders;

public class Cart : BaseEntity<Guid>
{
    public DateTime StartFrom { get; set; }
    public DateTime? ExpiredFrom { get; set; }
    public StatusCart StatusCart { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

}

