using Model.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Categories;

public class ProductFeatureValue : BaseEntity<int>
{
    public string FeatureValue { get; set; }
    public int CategoryFeatureId { get; set; }
    public virtual CategoryFeature CategoryFeature { get; set; }
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
}
