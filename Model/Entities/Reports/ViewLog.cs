using Model.Entities.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Reports;

public class ViewLog : BaseEntity<int>
{
    public string? IP { get; set; }
    public DateTime DateTime { get; set; }
    public string Url { get; set; }
    public Guid? userId { get; set; }
    public User? User { get; set; }

}
