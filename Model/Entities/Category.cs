using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Category
{
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public Guid CategoryParent {  get; set; }
    public Category Parent { get; set; }

}
