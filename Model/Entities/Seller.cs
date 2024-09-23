using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Seller
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string OwnerFirstName { get; set; } = string.Empty;
        public string OwnerLastName { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Customer> CustomerList { get; set; }

    }
}
