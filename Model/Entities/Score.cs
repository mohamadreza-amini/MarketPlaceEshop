using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Score : BaseEntity<Guid>
{
    public Byte StarRating {  get; set; }

    //ترکیب ProductSupplierId و customerid باید unique باشه
    public Guid ProductSupplierId { get; set; }
    public ProductSupplier ProductSupplier { get; set; }
    public  Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    //
}
