using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Person.Commands;

public class SupplierCommand
{
    [Required(ErrorMessage = "وارد کردن اطلاعات کاربر الزامی است.")]
    [Display(Name = "اطلاعات کاربری")]
    public UserCommand UserDto { get; set; }

    [Required(ErrorMessage = "وارد کردن شماره حساب شرکت است.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "شماره حساب باید شامل اعداد باشد.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "شماره حساب باید حداقل 1 و حداکثر 50 رقم باشد.")]
    [Display(Name = "شماره حساب شرکت")]
    public string BankAccountNumber { get; set; }

    [Required(ErrorMessage = "وارد کردن اسم شرکت الزامی است.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "نام شرکت باید حداقل 1 و حداکثر 50 کاراکتر باشد.")]
    [Display(Name = "نام شرکت")]
    public string CompanyName { get; set; }

    [Required(ErrorMessage = "وارد کردن شماره ثبت شرکت است.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "شماره ثبت شرکت باید حداقل 1 و حداکثر 50 کاراکتر باشد.")]
    [Display(Name = "شماره ثبت شرکت")]
    public string CompanyRegistrationNumber { get; set; }

}
