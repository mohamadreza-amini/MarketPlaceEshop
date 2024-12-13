using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Category.Results;

public class CategoryResult:BaseDTO<int>
{
    public string CategoryName { get; set; }
    public int? ParentCategoryId { get; set; }
    public int Level { get; set; }
    public string? ParentCategoryName { get; set; }
}
