using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.Person;
using Model.Entities.Products;
using Model.Exceptions;

namespace Model.Entities.Review;

public class Comment : BaseEntity<Guid>
{
    public string CommentText { get; set; }
    public DateTime DateOfRegistration { get; set; }
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(CommentText) ||
           CustomerId==Guid.Empty||
           ProductId==Guid.Empty)
        {
            throw new ModelValidationException("Comment");
        }
    }
}
