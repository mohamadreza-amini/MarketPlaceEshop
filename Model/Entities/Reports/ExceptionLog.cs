using Model.Entities.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Reports;

public class ExceptionLog : BaseEntity<int>
{
    public ExceptionLog(Exception ex, Guid userId)
    {
        ExceptionType = ex.GetType().Name;
        Message = ex.Message;
        InnerException = ex.InnerException?.Message;
        StackTrace = ex.StackTrace;
        DateTime = DateTime.Now;
        UserId = userId;
    }

    private ExceptionLog() { }

    public string ExceptionType { get; set; }
    public string Message { get; set; }
    public string? InnerException { get; set; }
    public string? StackTrace { get; set; }
    public DateTime DateTime { get; set; }
    public Guid? UserId { get; set; }
    public User? User { get; set; }

}
