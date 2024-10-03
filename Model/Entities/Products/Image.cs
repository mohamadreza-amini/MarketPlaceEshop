using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Products;

public class Image : BaseEntity<Guid>
{
    public string Path { get; set; }
    public byte Priority { get; set; }
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
}
