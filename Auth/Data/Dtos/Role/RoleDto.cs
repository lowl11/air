using Auth.Data.Entities;
using Storage.Data.Dtos;

namespace Auth.Data.Dtos.Role;

public class RoleDto : Dto
{

    public RoleDto(RoleEntity entity)
        : base(entity)
    {
        //
    }

    public static RoleDto FromEntity(RoleEntity entity) => new(entity);

}