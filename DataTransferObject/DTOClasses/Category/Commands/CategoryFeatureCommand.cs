using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Category.Commands;

public class CategoryFeatureCommand:BaseDTO<>
{
    public string FeatureName { get; set; }
    public int CategoryId { get; set; }
  
}
