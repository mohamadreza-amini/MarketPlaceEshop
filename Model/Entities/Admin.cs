using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities;

public class Admin : BaseEntity<Guid>
{
    public string FirstName {  get; set; }
    public string LastName { get; set; }
    public string Password {  get; set; }
    //یونیک باشه یا ایندکس
    public string MobileNumber { get; set; }


}
