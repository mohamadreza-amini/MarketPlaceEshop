using Model.Entities.Orders;
using Model.Entities.Person;
using Model.Exceptions;
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


    public void validate()
    {
        if(string.IsNullOrWhiteSpace(Neighborhood) || 
           string.IsNullOrWhiteSpace(AddressDetail) ||
           string.IsNullOrWhiteSpace(PostalCode) ||
           HouseNumber < 1 ||
           UnitNumber < 0 ||
           CityId == 0||
           CustomerId == Guid.Empty)
        {
            throw new ModelValidationException("Address");
        }
    }
}
