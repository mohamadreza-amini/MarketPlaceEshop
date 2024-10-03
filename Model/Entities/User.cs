using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class User : BaseEntity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MobileNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string NationalCode { get; set; }

    /*    public Guid? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public Guid? AdminId { get; set; }
        public Admin? Admin { get; set; }*/
}
