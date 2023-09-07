using Auth.Data.Dtos.UserSession;

namespace Air.Data.Models.UserSession;

public class UserSessionModel
{
    
    [JsonPropertyName("token")]
    public string Token { get; set; }
    
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("role")]
    public RoleSessionModel Role { get; set; }

    public UserSessionModel(UserSessionDto dto)
    {
        Token = dto.Token;
        Username = dto.Username;
        Role = RoleSessionModel.FromDto(dto.Role);
    }

    public static UserSessionModel FromDto(UserSessionDto dto) => new(dto);

}