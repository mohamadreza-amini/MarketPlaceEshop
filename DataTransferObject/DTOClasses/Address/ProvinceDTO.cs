using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Address;

public class ProvinceDTO : BaseDTO<int>
{
    public string ProvinceName { get; set; }
}
