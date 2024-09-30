using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class ProductFeatureValue : BaseEntity<int>
{
    // نرکیب کتگوری فیچر ایدی و پروداکت ایدی یونیک باشه
    public string FeatureValue { get; set; }
    public int CategoryFeatureId { get; set; }
    public CategoryFeature CategoryFeature { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}
