using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Review.Results;

public class ScoreResult:BaseDTO<Guid>
{
    public int StarRating { get; set; }
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
}
