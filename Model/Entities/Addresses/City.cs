using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Addresses;

public class City : BaseEntity<int>
{
    public string CityName { get; set; }
    public int ProvinceId { get; set; }
    public virtual Province Province { get; set; }
    public virtual ICollection<Address> Addresses { get; set; }

}
