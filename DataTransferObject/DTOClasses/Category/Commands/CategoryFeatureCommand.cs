using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Category.Commands;

public class CategoryFeatureCommand
{
    [Required(ErrorMessage = "وارد کردن نام وِیژگی الزامی است")]
    [RegularExpression(@"^(?=.*[a-zA-Z\u0600-\u06FF0-9]).*$", ErrorMessage = "ورودی صحیح نمی باشد")]
    [Display(Name = "ویژگی")]
    public string FeatureName { get; set; }
    public int CategoryId { get; set; }
  
}


