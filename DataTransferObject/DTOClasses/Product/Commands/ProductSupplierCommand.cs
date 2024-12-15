using DataTransferObject.DTOClasses.Category.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Product.Commands;

public class ProductSupplierCommand
{
    [Range(0, 2000000000)]
    [Required(ErrorMessage = "وارد کردن موجودی محصول الزامی است.")]
    [Display(Name = "موجودی محصول")]
    public int Ventory { get; set; }
    [Required(ErrorMessage = "وارد کردن مقدار تخفیف الزامی است")]
    [Display(Name = "تخفیف")]
    public decimal Discount { get; set; }

    [Display(Name = "فعال")]
    public bool IsDisable { get; set; }

    [Required(ErrorMessage = "وارد کردن قیمت الزامی است")]
    [Display(Name = "قیمت")]
    public decimal PriceValue { get; set; }
    public Guid SupplierId { get; set; }
    public Guid ProductId { get; set; }

}


