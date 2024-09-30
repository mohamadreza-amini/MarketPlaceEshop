using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Supplier : BaseEntity<Guid>
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    //پایینی یونیک باشه برای لاگین یا بررسی کن میشه ایدی همین باشه
    public string PhoneNumber { get; set; }
    public string MobileNumber { get; set; }
    public string Password { get; set; }
    public string BankAccountNumber { get; set; }
    public string ChairmanName { get; set; }
    public string CompanyRegistrationNumber { get; set; }
    //
    
    public ICollection<ProductSupplier> ProductSuppliers { get; set; }
}
