using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Person.Commands;

public class UserCommand : BaseDTO<Guid>
{

    [Length(1, 50)]
    [Required(ErrorMessage = "وارد کردن نام الزامی است.")]
    [RegularExpression("[آ-یA-Za-z]+", ErrorMessage = "نام باید فقط شامل حروف فارسی، انگلیسی باشد.")]
    [Display(Name = "نام")]
    public string FirstName { get; set; }


    [Length(1, 50)]
    [Required(ErrorMessage = "وارد کردن نام خانوادگی الزامی است.")]
    [RegularExpression("[آ-یA-Za-z]+", ErrorMessage = "نام خانوادگی باید فقط شامل حروف فارسی، انگلیسی باشد.")]
    [Display(Name = "نام خانوادگی")]
    public string LastName { get; set; }


    [Length(1, 30)]
    [Required(ErrorMessage = "وارد کردن شماره موبایل الزامی است.")]
    [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره موبایل صحیح نیست.")]
    [Phone]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "شماره موبایل")]
    public string MobileNumber { get; set; }

    [Required(ErrorMessage = "وارد کردن شماره تلفن الزامی است.")]
    [Length(1, 30)]
    [Phone]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "شماره تلفن")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "وارد کردن پسورد الزامی است.")]
    [Length(6, 100)]
    [DataType(DataType.Password)]
    [Display(Name = "رمز عبور")]
    public string Password { get; set; }

    [Required(ErrorMessage = "وارد کردن تکرار پسورد الزامی است.")]
    [Length(6, 100)]
    [Compare("Password", ErrorMessage = "تکرار پسورد صحیح نیست")]
    [DataType(DataType.Password)]
    [Display(Name = "تکرار رمز عبور")]
    public string ConfirmPassword { get; set; }

    [Required]
    [EmailAddress]
    [Length(1, 100)]
    [Display(Name = "ایمیل")]
    public string Email { get; set; }


    [Required(ErrorMessage = "وارد کردن تاریخ تولد الزامی است.")]
    [DataType(DataType.Date)]
    [Display(Name = "تاریخ تولد")]
    public DateTime DateOfBirth { get; set; }


    [Required(ErrorMessage = "وارد کردن کد ملی الزامی است.")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "کد ملی باید 10 رقم باشد.")]
    [RegularExpression("^[0-9]+$", ErrorMessage = "کد ملی باید فقط شامل اعداد باشد.")]
    [Display(Name = "کد ملی")]
    public string NationalCode { get; set; }


}
