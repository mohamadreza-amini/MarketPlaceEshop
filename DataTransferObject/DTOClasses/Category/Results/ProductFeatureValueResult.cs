using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Category.Results;

public class ProductFeatureValueResult:BaseDTO<int>
{
    public string FeatureValue { get; set; }
    public string FeatureName { get; set; }

}
