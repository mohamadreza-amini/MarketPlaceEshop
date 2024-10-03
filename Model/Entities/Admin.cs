using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Admin : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
