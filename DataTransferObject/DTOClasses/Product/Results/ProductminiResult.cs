using DataTransferObject.DTOClasses.Category.Results;
using DataTransferObject.DTOClasses.Review.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Product.Results;

public class ProductminiResult:BaseDTO<Guid>
{
    public string Titel { get; set; }
    public string Name { get; set; }   
    public double AverageScore { get; set; }
    public string ImagePath { get; set; }
    public bool Discount { get; set; }
    public decimal Price { get; set; }
}
