using Exceptions.Base;

namespace Exceptions.Auth;

public class NoTokenContentException : BaseException
{

    public NoTokenContentException(string token)
        : base($"No auth token: {token}")
    {
    }
    
}