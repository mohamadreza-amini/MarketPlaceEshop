using Model.Entities.Addresses;
using Model.Entities.Person;
using Model.Entities.Products;
using Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Categories;

public class Category : BaseEntity<int>
{
    public string CategoryName { get; set; }
    public int Level { get; set; }
    public int? ParentCategoryId { get; set; }
    public virtual Category? ParentCategory { get; set; }
    public virtual ICollection<Category>? ChildCategories { get; set; }
    public virtual ICollection<CategoryFeature>? CategoryFeatures { get; set; }
    public virtual ICollection<Product>? Products { get; set; }

    public void validate()
    {
        if (string.IsNullOrWhiteSpace(CategoryName) ||
           Level < 0 )
        {
            throw new ModelValidationException("Category");
        }
    }
}
