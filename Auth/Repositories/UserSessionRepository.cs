using Auth.Data.Entities;
using Auth.Data.Interfaces.Repositories;
using Exceptions.Crud;
using Microsoft.Extensions.Caching.Memory;

namespace Auth.Repositories;

public class UserSessionRepository : IUserSessionRepository
{

    private readonly IMemoryCache _cache;

    public UserSessionRepository(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void Create(string key, UserSessionEntity entity)
    {
        _cache.Set(key, entity);
    }

    public UserSessionEntity Get(string key)
    {
        if (!_cache.TryGetValue(key, out var result) || result == null)
        {
            throw new NotFoundException(typeof(UserSessionEntity));
        }

        return (UserSessionEntity)result;
    }
    
}