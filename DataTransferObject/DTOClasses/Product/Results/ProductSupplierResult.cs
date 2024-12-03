using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Product.Results;

public class ProductSupplierResult:BaseDTO<Guid>
{
    public int Ventory { get; set; }
    public decimal Discount { get; set; }
    public Guid SupplierId { get; set; }
    public string CompanyName { get; set; }
    public Guid ProductId { get; set; }
    public int Scores { get; set; }
    public decimal Prices { get; set; }

}
