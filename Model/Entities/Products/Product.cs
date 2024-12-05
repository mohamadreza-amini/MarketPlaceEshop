using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.Categories;
using Model.Entities.Review;
using Model.Exceptions;

namespace Model.Entities.Products;

public class Product : BaseEntity<Guid>
{
    public string Titel { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public int View { get; set; }
    public bool IsDisable { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public int BrandId { get; set; }
    public virtual Brand Brand { get; set; }
    public virtual ICollection<Comment>? Comments { get; set; }
    public virtual ICollection<Score>? Scores { get; set; }
    public virtual ICollection<ProductSupplier> productSuppliers { get; set; }
    public virtual ICollection<Image> Images { get; set; }
    public virtual ICollection<ProductFeatureValue> ProductFeatureValues { get; set; }


    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Titel) ||
            string.IsNullOrWhiteSpace(Name) ||
           CategoryId <= 0 ||
           BrandId <= 0)
        {
            throw new ModelValidationException("Product");
        }
    }

}
