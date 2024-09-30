using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Comment : BaseEntity<Guid>
{
    public string CommentText { get; set; }
    public DateTime DateOfRegistration { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public Guid ProductSupplierId { get; set; }
    public ProductSupplier ProductSupplier { get; set; }
    //
}
