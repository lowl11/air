using Exceptions.Base;

namespace Exceptions.Auth;

public class DifferentPasswordsException : BaseException
{

    public DifferentPasswordsException()
        : base("Passwords are different")
    {
    }
    
}