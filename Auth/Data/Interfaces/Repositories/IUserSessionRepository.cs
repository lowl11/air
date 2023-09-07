using Auth.Data.Entities;

namespace Auth.Data.Interfaces.Repositories;

public interface IUserSessionRepository
{

    void Create(string key, UserSessionEntity entity);
    UserSessionEntity Get(string key);

}