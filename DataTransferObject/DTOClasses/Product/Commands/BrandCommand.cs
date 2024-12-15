using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Product.Commands;

public class BrandCommand
{
    [StringLength(50, ErrorMessage = "حداکثر 50 کاراکتر")]
    [Required(ErrorMessage = "وارد کردن نام برند الزامی است.")]
    [RegularExpression(@"^(?=.*[a-zA-Z\u0600-\u06FF]).*$", ErrorMessage = "نام باید فقط شامل حروف فارسی، انگلیسی باشد.")]
    [Display(Name = "برند")]
    public string BrandName { get; set; }
}
