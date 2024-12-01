using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Address.Results;

public class ProvinceResult : BaseDTO<int>
{
    [Display(Name = "استان")]
    public string ProvinceName { get; set; }
}
