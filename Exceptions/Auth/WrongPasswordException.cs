using Exceptions.Base;

namespace Exceptions.Auth;

public class WrongPasswordException : BaseException
{

    public WrongPasswordException()
        : base("Given password is wrong")
    {
    }
    
}