using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Price
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public decimal price {  get; set; }

}
