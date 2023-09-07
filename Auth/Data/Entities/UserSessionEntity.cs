using System.Text.Json.Serialization;

namespace Auth.Data.Entities;

public class UserSessionEntity
{
    
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("role")]
    public RoleSessionEntity Role { get; set; }
    
}