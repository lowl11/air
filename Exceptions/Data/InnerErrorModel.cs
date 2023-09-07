using System.Text.Json.Serialization;

namespace Exceptions.Data;

public class InnerErrorModel
{
    
    [JsonPropertyName("title")]
    public string Message { get; set; }
    
    [JsonPropertyName("message")]
    public string? Stack { get; set; }

    public InnerErrorModel(Exception exception)
    {
        Message = exception.Message;
        Stack = exception.StackTrace;
    }
    
}