using Auth.Data.Dtos.UserSession;

namespace Air.Data.Models.UserSession;

public class RoleSessionModel
{
    
    [JsonPropertyName("code")]
    public string Code { get; set; }

    public RoleSessionModel(RoleSessionDto dto)
    {
        Code = dto.Code;
    }

    public static RoleSessionModel FromDto(RoleSessionDto dto) => new(dto);

}