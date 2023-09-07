using Auth.Data.Contexts;
using Auth.Data.Entities;
using Auth.Data.Interfaces.Repositories;
using Storage.Repositories;

namespace Auth.Repositories;

public class RoleRepository : CrudRepository<RoleEntity>, IRoleRepository
{

    public RoleRepository(AuthContext context)
        : base(context)
    {
    }
    
}