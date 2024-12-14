using Model.Entities.Categories;
using Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Model.Entities.Products;

public class Image : BaseEntity<Guid>
{
    public string Path { get; set; }
    //پیاده نشده
    public int Priority { get; set; }
    public Guid ProductId { get; set; }
    public string MimeType { get; set; }
    public int FileSize { get; set; }
    public virtual Product Product { get; set; }


    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Path))
        {
            throw new ModelValidationException("Image");
        }
    }
}
