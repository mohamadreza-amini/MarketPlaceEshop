using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.Person;
using Model.Entities.Products;

namespace Model.Entities.Review;

public class Score : BaseEntity<Guid>
{
    public byte StarRating { get; set; }
    public Guid ProductSupplierId { get; set; }
    public virtual ProductSupplier ProductSupplier { get; set; }
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}
