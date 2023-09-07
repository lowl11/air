using Auth.Data.Contexts;
using Auth.Data.Entities;
using Auth.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Storage.Repositories;

namespace Auth.Repositories;

public class UserRepository : CrudRepository<UserEntity>, IUserRepository
{

    public UserRepository(AuthContext context)
        : base(context)
    {
        ApplyQuery(users 
            => users.Include(user => user.Role));
    }

    public async Task<UserEntity?> GetByUsernameAndPassword(string username, string password)
        => await SingleOrDefault(user => user.Username == username && user.Passowrd == password);

}