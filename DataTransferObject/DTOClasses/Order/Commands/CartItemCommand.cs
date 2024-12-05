using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Order.Commands;

public class CartItemCommand
{
    [Range(1,1000,ErrorMessage ="تعداد باید بیشتر از یک باشه")]
    [Required(ErrorMessage = "وارد کردن تعداد الزامی است.")]
    [Display(Name = "تعداد")]
    public int Quantity { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ProductSupplierId { get; set; }
}

//برای تعداد دیفالت مقداردهی کنم یک بزارم برای حالت دیفالت یک باشه وقتی دکمه خرید بدون تعداد زده میشه




