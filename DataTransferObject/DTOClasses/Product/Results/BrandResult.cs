using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Product.Results;

public class BrandResult : BaseDTO<int>
{
    public string BrandName { get; set; }
}
