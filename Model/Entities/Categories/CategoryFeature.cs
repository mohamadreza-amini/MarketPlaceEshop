using Model.Entities.Addresses;
using Model.Entities.Person;
using Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Categories;

public class CategoryFeature : BaseEntity<int>
{
    public string FeatureName { get; set; }
    public byte Priority { get; set; }
    public bool Filterable { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<ProductFeatureValue>? ProductFeatureValues { get; set; }



    public void validate()
    {
        if (string.IsNullOrWhiteSpace(FeatureName) ||
           CategoryId < 0)
        {
            throw new ModelValidationException("CategoryFeature");
        }
    }
}
