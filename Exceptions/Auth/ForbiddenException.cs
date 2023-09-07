using Exceptions.Base;

namespace Exceptions.Auth;

public class ForbiddenException : BaseException
{

    public ForbiddenException(string? resource = null)
        : base($"Forbidden resource{(resource is not null ? $" {resource}" : string.Empty)}")
    {
    }
    
}