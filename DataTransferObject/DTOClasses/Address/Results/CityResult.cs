using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Address.Results;

public class CityResult : BaseDTO<int>
{
    [Display(Name = "شهر")]
    public string CityName { get; set; }
}
