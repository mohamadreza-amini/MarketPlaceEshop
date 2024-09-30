using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Category : BaseEntity<int>
{
    public string CategoryName {  get; set; }
    public int CategoryParent {  get; set; }
    // باشه لول یا نه
    public byte Level { get; set; }

}
