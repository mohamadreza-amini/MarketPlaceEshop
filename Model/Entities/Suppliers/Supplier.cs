using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Suppliers;

public class Supplier : BaseEntity<Guid>
{
    public DateTime StartDate { get; set; }
    public string BankAccountNumber { get; set; }
    public string CompanyName { get; set; }
    public string CompanyRegistrationNumber { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<ProductSupplier>? ProductSuppliers { get; set; }
}
