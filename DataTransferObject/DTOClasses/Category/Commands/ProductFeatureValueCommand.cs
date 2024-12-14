using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Category.Commands;

public class ProductFeatureValueCommand
{
    [Required(ErrorMessage = "وارد کردن مقدار وِیژگی الزامی است")]
    [RegularExpression(@"^(?=.*[a-zA-Z\u0600-\u06FF0-9]).*$", ErrorMessage = "ورودی صحیح نمی باشد")]
    [Display(Name = "مقدار ویژگی")]
    public string FeatureValue { get; set; }
    public int CategoryFeatureId { get; set; }
    public string FeatureName { get; set; }

}
