using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Review.Results;

public class CommentResult:BaseDTO<Guid>
{
    public string CommentText { get; set; }
    public DateTime DateOfRegistration { get; set; }
    public Guid CustomerId { get; set; }
    public string FullName { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
}
