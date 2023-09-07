using Auth.Data.Entities;

namespace Auth.Data.Dtos.UserSession;

public class UserSessionDto
{
    
    public string Token { get; set; }
    
    public string Username { get; set; }
    
    public RoleSessionDto Role { get; set; }

    public UserSessionDto(string token, UserSessionEntity entity)
    {
        Token = token;
        Username = entity.Username;
        Role = RoleSessionDto.FromEntity(entity.Role);
    }

    public UserSessionDto(string token, string username, RoleEntity role)
    {
        Token = token;
        Username = username;
        Role = RoleSessionDto.FromRoleEntity(role);
    }

    public static UserSessionDto FromEntity(string token, UserSessionEntity entity) 
        => new(token, entity);

    public UserSessionEntity ToEntity()
    {
        return new UserSessionEntity()
        {
            Username = Username,
            Role = Role.ToEntity(),
        };
    }

}