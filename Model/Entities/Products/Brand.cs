﻿using Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Products;

public class Brand : BaseEntity<int>
{
    public string BrandName { get; set; }
    public virtual ICollection<Product>? products { get; }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(BrandName))
        {
            throw new ModelValidationException("brand");
        }
    }

}
