using Auth.Data.Dtos.User;
using Auth.Data.Dtos.UserSession;

namespace Auth.Data.Interfaces.Services;

public interface IUserService
{
    
    Task<UserSessionDto> LoginByCredentials(UserLoginByCredentialsDto dto);
    UserSessionDto LoginByToken(UserLoginByTokenDto dto);
    
}