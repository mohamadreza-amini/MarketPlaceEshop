using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Product.Commands;

public class BrandCommand
{
    [Length(1, 50)]
    [Required(ErrorMessage = "وارد کردن نام برند الزامی است.")]
    [RegularExpression("[آ-یA-Za-z]+", ErrorMessage = "نام باید فقط شامل حروف فارسی، انگلیسی باشد.")]
    [Display(Name = "برند")]
    public string BrandName { get; set; }
}
