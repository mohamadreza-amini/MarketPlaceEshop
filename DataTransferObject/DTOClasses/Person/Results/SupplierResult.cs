using DataTransferObject.DTOClasses.Person.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Person.Results;

public class SupplierResult:BaseDTO<Guid>
{
    [Display(Name = "نام")]
    public string FirstName { get; set; }

    [Display(Name = "نام خانوادگی")]
    public string LastName { get; set; }

    [Phone]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "شماره موبایل")]
    public string MobileNumber { get; set; }

    [Phone]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "شماره تلفن")]
    public string PhoneNumber { get; set; }

    [EmailAddress]
    [Display(Name = "ایمیل")]
    public string Email { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "تاریخ تولد")]
    public DateTime DateOfBirth { get; set; }

    [Display(Name = "کد ملی")]
    public string NationalCode { get; set; }
   
    [Display(Name = "شماره حساب شرکت")]
    public string BankAccountNumber { get; set; }

    [Display(Name = "نام شرکت")]
    public string CompanyName { get; set; }

    [Display(Name = "شماره ثبت شرکت")]
    public string CompanyRegistrationNumber { get; set; }

    [Display(Name = "تاریخ شروع فعالیت")]
    public DateTime StartDate { get; set; }

}
