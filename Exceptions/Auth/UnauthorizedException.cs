using Exceptions.Base;

namespace Exceptions.Auth;

public class UnauthorizedException : BaseException
{

    public UnauthorizedException(Exception inner)
        : base("Unauthorized")
    {
        Add("inner", new InnerErrorModel(inner));
    }
    
}