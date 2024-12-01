using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Category.Commands;

public class CategoryCommand
{
    public string CategoryName { get; set; }
    public int ParentCategoryId { get; set; }
}
