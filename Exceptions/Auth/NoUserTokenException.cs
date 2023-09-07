using Exceptions.Base;

namespace Exceptions.Auth;

public class NoUserTokenException : BaseException
{

    public NoUserTokenException()
        : base("No User auth token")
    {
    }
    
}