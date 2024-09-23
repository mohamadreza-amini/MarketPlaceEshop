using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Like
{
    public Guid CustomerID { get; set; }
    public Guid ProductId { get; set; }
    public Customer Customer { get; set; }  
    public Product Product { get; set; }
}
