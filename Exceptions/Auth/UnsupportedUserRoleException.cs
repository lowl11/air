using Exceptions.Base;

namespace Exceptions.Auth;

public class UnsupportedUserRoleException : BaseException
{
    
    public UnsupportedUserRoleException()
        : base("Unsupported user role")
    {
    }
    
}