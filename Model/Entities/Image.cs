using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Image : BaseEntity<Guid>
{
    public string Path { get; set; }
    public Byte Priority { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}
