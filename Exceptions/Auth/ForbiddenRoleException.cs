using Exceptions.Base;

namespace Exceptions.Auth;

public class ForbiddenRoleException : BaseException
{

    public ForbiddenRoleException()
        : base("Forbidden to change user role to given")
    {
    }

}