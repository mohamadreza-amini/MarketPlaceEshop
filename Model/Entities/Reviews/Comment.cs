using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.Person;
using Model.Entities.Products;

namespace Model.Entities.Review;

public class Comment : BaseEntity<Guid>
{
    public string CommentText { get; set; }
    public DateTime DateOfRegistration { get; set; }
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public Guid ProductSupplierId { get; set; }
    public virtual ProductSupplier ProductSupplier { get; set; }
}
