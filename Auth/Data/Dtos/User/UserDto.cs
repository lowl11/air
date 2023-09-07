using Auth.Data.Entities;
using Storage.Data.Dtos;

namespace Auth.Data.Dtos.User;

public class UserDto : Dto
{

    public UserDto(UserEntity entity)
        : base(entity)
    {
        //
    }

    public static UserDto FromEntity(UserEntity entity) => new(entity);

}