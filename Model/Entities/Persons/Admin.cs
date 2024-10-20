using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Person;

public class Admin : BaseEntity<Guid>
{
    public virtual User User { get; set; }
}
