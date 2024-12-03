using DataTransferObject.DTOClasses.Category.Commands;
using DataTransferObject.DTOClasses.Category.Results;
using DataTransferObject.DTOClasses.Product.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Product.Results;

public class ProductResult:BaseDTO<Guid>
{
    public string Titel { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public string? Description { get; set; }
    public string CategoryName { get; set; }
    public string BrandName { get; set; }
    public List<ImageResult> Images { get; set; }
    public List<ProductFeatureValueResult> ProductFeatureValues { get; set; }
    public List<ProductSupplierResult> productSuppliers { get; set; }
 
}



