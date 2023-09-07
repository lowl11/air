namespace Exceptions.Services;

public class Error
{

    private readonly string _message;
    private readonly int _status;
    private readonly string? _stack;

    public Error(string message, int status, string? stack = null)
    {
        _message = message;
        _status = status;
        _stack = stack;
    }

    public ErrorModel Get()
    {
        return Get(_message, _status, _stack);
    }

    public static ErrorModel Get(string message, int statusCode, string? stack = null)
    {
        var model = Create();
        
        model.Message = message;
        model.Status = statusCode;
        if (stack != null)
        {
            model.Stack = stack;
        }
        
        return model;
    }

    public static void FillContext(HttpContext context, ErrorModel error)
    {
        context.Response.StatusCode = error.Status;
    }

    private static ErrorModel Create()
    {
        return new ErrorModel()
        {
            Status = StatusCodes.Status500InternalServerError,
        };
    }
    
}