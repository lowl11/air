using System.Collections;
using System.Text.Json.Serialization;

namespace Exceptions.Data;

public sealed class ErrorModel
{
    
    /// <summary>
    /// Статус ответа (response)
    /// </summary>
    /// <example>404</example>
    [JsonPropertyName("status")]
    public int Status { get; set; }
    
    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    /// <example>Post not found</example>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    
    /// <summary>
    /// Стектрейс который привел к ошибке
    /// </summary>
    [JsonPropertyName("stack")]
    public string? Stack { get; set; }
    
    /// <summary>
    /// Контекст ошибки: какие либо дополнительные переменные или объекты
    /// </summary>
    [JsonPropertyName("context")]
    public Hashtable Context { get; set; }
    
}