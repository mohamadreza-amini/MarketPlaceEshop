using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Address.Results;

public class AddressResult:BaseDTO<Guid>
{   
    [Display(Name = "محله")]
    public string Neighborhood { get; set; }

    [Display(Name = "آدرس")]
    public string AddressDetail { get; set; }

    [Display(Name = "پلاک")]
    public int HouseNumber { get; set; }
   
    [Display(Name = "واحد")]
    public int UnitNumber { get; set; }
  
    [Display(Name = "کد پستی")]
    public string PostalCode { get; set; }
    public int CityId { get; set; }

    [Display(Name = "استان")]
    public string ProvinceName { get; set; }

    [Display(Name = "شهر")]
    public string CityName { get; set; }
}
