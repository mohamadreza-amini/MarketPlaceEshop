using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Role : BaseEntity<Guid>
{
    public string RoleName {  get; set; }
    public string RoleDescription {  get; set; }

}
