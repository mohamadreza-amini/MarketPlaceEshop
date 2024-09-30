using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Cart : BaseEntity<Guid>
{
    public DateTime StartFrom { get; set; }
    public DateTime ExpiredFrom { get; set; }
    // اینجوری درسته؟ enum
    public Status Status {  get; set; }
    public ICollection<CartItem> CartItems { get; set; }
    public Guid CustomerId {  get; set; }
    public Customer Customer { get; set; }

}


public enum Status
{
    Open,
    Canceled,
    Finalized
}