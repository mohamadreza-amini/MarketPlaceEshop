using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions;

public class ModelValidationException : BaseException
{
    public ModelValidationException(string? message=null) : base(message + "(Model Validation Exception)") {}
}
