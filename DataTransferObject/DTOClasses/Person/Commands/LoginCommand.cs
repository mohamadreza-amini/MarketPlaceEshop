using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Person.Commands
{
    public class LoginCommand
    {
        [Required(ErrorMessage ="وارد کردن ایمیل الزامی است")]
        [EmailAddress(ErrorMessage ="ایمیل نامعتبر")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "وارد کردن رمز الزامی است")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "به خاطر بسپار")]
        public bool RememberMe { get; set; }

    }
}
