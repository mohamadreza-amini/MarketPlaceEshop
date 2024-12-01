using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Person.Results;

public class UserResult:BaseDTO<Guid>
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

}
