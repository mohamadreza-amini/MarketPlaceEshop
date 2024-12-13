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
    [RegularExpression("[آ-یA-Za-z0-9\\s]+", ErrorMessage = "ورودی صحیح نمی باشد")]
    [Display(Name = "ویژگی")]
    public string FeatureName { get; set; }
    public int CategoryId { get; set; }
  
}


