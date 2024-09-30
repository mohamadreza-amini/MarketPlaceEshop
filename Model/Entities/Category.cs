using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Category : BaseEntity<int>
{
    public string CategoryName {  get; set; }

    // باشه لول یا نه
    public byte Level { get; set; }
    public int ParentCategoryId {  get; set; }
    public Category ParentCategory { get; set; }

    public ICollection<Category> ChildCategories { get; set;}

    public ICollection<CategoryFeature> CategoryFeatures { get; set; }
    public ICollection<Product> Products { get; set; }
 
}
