using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Product : BaseEntity<Guid>
{
    public string Titel { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public int View {  get; set; }
    public bool IsDisable { get; set; }
    public int CategoryId {  get; set; }
    public Category Category { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
    public ICollection<ProductSupplier> productSuppliers { get; set; }
    public ICollection<Image> Images { get; set; }
    public ICollection<ProductFeatureValue> ProductFeatureValues { get; set; }

}
