using DataTransferObject.DTOClasses.Category.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Product.Commands;

public class ProductCommand
{

    [Length(1, 50)]
    [Required(ErrorMessage = "وارد کردن عنوان محصول الزامی است.")]
    [RegularExpression(@"^(?=.*[a-zA-Z\u0600-\u06FF0-9]).*$", ErrorMessage = "عنوان محصول باید شامل حروف فارسی، انگلیسی باشد.")]
    [Display(Name = " عنوان محصول")]
    public string Titel { get; set; }

    [Length(1, 50)]
    [Required(ErrorMessage = "وارد کردن نام محصول الزامی است.")]
    [RegularExpression(@"^(?=.*[a-zA-Z\u0600-\u06FF0-9]).*$", ErrorMessage = "نام محصول باید شامل حروف فارسی، انگلیسی باشد.")]
    [Display(Name = " نام محصول")]
    public string Name { get; set; }
 
    [Display(Name = " توضیحات محصول")]
    [Required(ErrorMessage = "وارد کردن توضیحات الزامی است.")]
    public string? Description { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public List<ImageCommand> Images { get; set; } = new List<ImageCommand>();
    public List<ProductFeatureValueCommand> ProductFeatureValues { get; set; } 
    public string CategoryName  { get; set; }
    public string BrandName  { get; set; }

}
