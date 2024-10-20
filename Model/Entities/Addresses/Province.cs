using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Addresses;

public class Province : BaseEntity<int>
{
    public string ProvinceName { get; set; }
    public virtual ICollection<City> cities { get; set; }
}
