using Auth.Data.Dtos.User;
using Auth.Data.Dtos.UserSession;
using Auth.Data.Enums;
using Auth.Data.Interfaces.Services;
using Exceptions.Auth;
using Microsoft.AspNetCore.Http;

namespace Auth.Services;

public class UserAccessService
{
    
    public void Check(HttpContext context, Role required)
    {
        var session = (UserSessionDto)context.Items["user_session"];
        if (!Enum.TryParse(session.Role.Code, out Role role))
        {
            throw new UnsupportedUserRoleException();
        }
        if (!CheckRole(required, role)) ThrowForbidden();
    }

    public void Check(Role required, Role session)
    {
        if (!CheckRole(required, session)) ThrowForbidden();
    }

    private static bool CheckRole(Role required, Role session)
        => session >= required;

    private static void ThrowForbidden()
        => throw new ForbiddenException();
    
}