using Exceptions.Auth;
using Exceptions.Base;
using Exceptions.Crud;
using Exceptions.Services;

namespace Air.Middlewares;

public class ExceptionHandlingMiddleware
{

    private ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedException exception)
        {
            await ReturnError(context, exception, StatusCodes.Status401Unauthorized);
        }
        catch (ForbiddenException exception)
        {
            await ReturnError(context, exception, StatusCodes.Status403Forbidden);
        }
        catch (NotFoundException exception)
        {
            await ReturnError(context, exception, StatusCodes.Status404NotFound);
        }
        catch (Exception exception)
        {
            var error = Error.Get(exception.Message, StatusCodes.Status500InternalServerError, exception.StackTrace);
            _logger.LogError("Unhandled error: {ExceptionMessage}", exception.Message);
            Error.FillContext(context, error);
            await context.Response.WriteAsJsonAsync(error);
        }
    }

    private static async Task ReturnError(HttpContext context, BaseException exception, int status)
    {
        var error = Error.Get(exception.Message, status);
        error.Context = exception.GetContext();
        Error.FillContext(context, error);
        await context.Response.WriteAsJsonAsync(error);
    }
    
}