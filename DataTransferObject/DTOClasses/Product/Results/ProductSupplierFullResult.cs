using DataTransferObject.DTOClasses.Review.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Product.Results;

public class ProductSupplierFullResult: ProductSupplierResult
{
    public string ProductName { get; set; }
    public string Titel { get; set; }
    public DateTime StartDate { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string BrandName { get; set; }
    public double AverageScore { get; set; }
}
