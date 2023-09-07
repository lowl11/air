using Auth.Data.Entities;

namespace Auth.Data.Dtos.UserSession;

public class RoleSessionDto
{
    
    public string Code { get; set; }

    public RoleSessionDto(RoleSessionEntity entity)
    {
        Code = entity.Code;
    }

    public RoleSessionDto(RoleEntity entity)
    {
        Code = entity.Code;
    }

    public static RoleSessionDto FromEntity(RoleSessionEntity entity) => new(entity);
    public static RoleSessionDto FromRoleEntity(RoleEntity entity) => new(entity);

    public RoleSessionEntity ToEntity()
    {
        return new RoleSessionEntity()
        {
            Code = Code,
        };
    }

}