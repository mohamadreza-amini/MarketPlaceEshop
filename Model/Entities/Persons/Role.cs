using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Person;

public class Role : IdentityRole<Guid>
{
    public string RoleDescription { get; set; }
    public bool IsDeleted { get; set; } = false;
    public Guid? CreatorUserId { get; set; }
    public virtual User? CreatorUser { get; set; }
    public Guid? UpdaterUserId { get; set; }
    public virtual User? UpdaterUser { get; set; }
    public DateTime? CreateDatetime { get; set; }
    public DateTime? UpdateDatetime { get; set; }
}
