using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Exceptions;

public class BaseException : Exception
{
    public BaseException(string? message) : base(message) { }
}


public class AccessDeniedException : BaseException
{
    public AccessDeniedException(string? message=null) : base("Access Denied!" + message) { }
}




public class IdentityException : BaseException
{
    public IdentityException(string? message) : base(message)
    {
    }
}


public class RegisterException : IdentityException
{
    public RegisterException(string? message) : base(message)
    {
    }
}


public class SignInException : IdentityException
{
    public SignInException(string? message) : base(message)
    {
    }
}