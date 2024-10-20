using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.Orders;
using Model.Entities.Review;
using Model.Entities.Addresses;

namespace Model.Entities.Person;

public class Customer : BaseEntity<Guid>
{
    public DateTime RegisterDate { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<Address> Addresses { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }
    public virtual ICollection<Comment>? Comments { get; set; }
    public virtual ICollection<Score>? Scores { get; set; }
    public virtual ICollection<CartItem>? CartItem { get; set; }

}
