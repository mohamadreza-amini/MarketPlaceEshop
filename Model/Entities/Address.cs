using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Address : BaseEntity<Guid>
{
    public string Province { get; set; }
    public string City { get; set; }
    public string Neighborhood { get; set; }
    public string AddressDetail { get; set; }
    public int HouseNumber { get; set; }
    public int UnitNumber {  get; set; } 
    public string PostalCode { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }

    public ICollection<Order> Orders { get; set; }
}
