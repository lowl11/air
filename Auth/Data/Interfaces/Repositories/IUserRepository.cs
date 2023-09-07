using Auth.Data.Entities;
using Storage.Data.Interfaces;

namespace Auth.Data.Interfaces.Repositories;

public interface IUserRepository : ICrudRepository<UserEntity>
{

    Task<UserEntity?> GetByUsernameAndPassword(string username, string password);

}