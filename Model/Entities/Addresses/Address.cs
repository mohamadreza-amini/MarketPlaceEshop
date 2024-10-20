using Model.Entities.Orders;
using Model.Entities.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Addresses;

public class Address : BaseEntity<Guid>
{
    public string Neighborhood { get; set; }
    public string AddressDetail { get; set; }
    public int HouseNumber { get; set; }
    public int UnitNumber { get; set; }
    public string PostalCode { get; set; }
    public Guid CustomerId { get; set; }
    public int CityId { get; set; }
    public virtual City City { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }
}
