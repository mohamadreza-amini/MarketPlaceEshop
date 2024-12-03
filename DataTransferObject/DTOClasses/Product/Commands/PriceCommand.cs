using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Product.Commands;

public class PriceCommand
{
    public decimal PriceValue { get; set; }
    public Guid ProductSupplierId { get; set; }
}
