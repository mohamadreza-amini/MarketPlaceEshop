﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.Person;
using Model.Entities.Products;
using Model.Exceptions;

namespace Model.Entities.Review;

public class Score : BaseEntity<Guid>
{
    public int StarRating { get; set; }
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public void Validate()
    {
        if (ProductId == Guid.Empty ||
            CustomerId == Guid.Empty ||
            StarRating > 5 ||
            StarRating < 1)
        {
            throw new ModelValidationException("Score");
        }
    }
}
