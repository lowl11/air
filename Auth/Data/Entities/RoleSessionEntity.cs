using System.Text.Json.Serialization;

namespace Auth.Data.Entities;

public class RoleSessionEntity
{
    
    [JsonPropertyName("code")]
    public string Code { get; set; }
    
}