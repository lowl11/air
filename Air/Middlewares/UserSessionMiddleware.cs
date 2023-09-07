using System.Collections.ObjectModel;
using Auth.Data.Dtos.User;
using Auth.Data.Interfaces.Services;
using Exceptions.Auth;

namespace Air.Middlewares;

public class UserSessionMiddleware
{
    
    private static readonly ReadOnlyCollection<string> IgnoreList = new(
        new[]{
            "api/v1/auth/login",
        }
    );
    
    private readonly ILogger<UserSessionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public UserSessionMiddleware(
        ILogger<UserSessionMiddleware> logger,
        RequestDelegate next
    ) {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserService userService)
    {
        if (IgnoreList.
                SingleOrDefault(str 
                    => context.Request.Path.ToString().Contains(str)) is not null)
        {
            await _next(context);
            return;
        }
        
        try
        {
            string? authorizationToken = context.Request.Headers["Authorization"];
            if (authorizationToken is null) throw new NoUserTokenException();

            var token = authorizationToken!.Split(" ")[1];
            var session = userService.LoginByToken(new UserLoginByTokenDto()
            {
                Token = token,
            });
            context.Items["user_token"] = token;
            context.Items["user_session"] = session;
        }
        catch (Exception exception)
        {
            throw new UnauthorizedException(exception);
        }
        
        await _next(context);
    }
    
}