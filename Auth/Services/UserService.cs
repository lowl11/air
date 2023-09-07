using Auth.Data.Dtos.User;
using Auth.Data.Dtos.UserSession;
using Auth.Data.Entities;
using Auth.Data.Interfaces.Repositories;
using Auth.Data.Interfaces.Services;
using Exceptions.Auth;
using Exceptions.Crud;

namespace Auth.Services;

public class UserService : IUserService
{

    private const string UserSessionKey = "user_session_{0}";

    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserPasswordService _passwordService;
    private readonly IUserSessionRepository _sessionRepository;

    public UserService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUserPasswordService passwordService,
        IUserSessionRepository sessionRepository
    ) {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordService = passwordService;
        _sessionRepository = sessionRepository;
    }

    public async Task<UserSessionDto> LoginByCredentials(UserLoginByCredentialsDto dto)
    {
        var encryptedPassword = _passwordService.Encrypt(dto.Password);
        var user = await _userRepository.GetByUsernameAndPassword(dto.Username, encryptedPassword);
        if (user is null)
        {
            throw new UnauthorizedException(new NotFoundException(typeof(UserEntity)));
        }

        var token = Guid.NewGuid().ToString();
        var session = new UserSessionDto(token, user.Username, user.Role);
        
        _sessionRepository.Create(string.Format(UserSessionKey, token), session.ToEntity());
        
        return session;
    }

    public UserSessionDto LoginByToken(UserLoginByTokenDto dto)
    {
        var searchToken = string.Format(UserSessionKey, dto.Token);
        return UserSessionDto.FromEntity(dto.Token, _sessionRepository.Get(searchToken));
    }
    
}