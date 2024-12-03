using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Product.Results;

public class ImageResult:BaseDTO<Guid>
{
    public string Path { get; set; }
    public Guid ProductId { get; set; }
   
}
