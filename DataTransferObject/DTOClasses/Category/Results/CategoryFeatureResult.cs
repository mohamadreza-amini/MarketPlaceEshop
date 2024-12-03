using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Category.Results;

public class CategoryFeatureResult:BaseDTO<int>
{
    public string FeatureName { get; set; }
    public int CategoryId { get; set; }
}
