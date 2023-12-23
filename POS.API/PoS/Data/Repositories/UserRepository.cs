using PoS.Abstractions;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Entities;

namespace PoS.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
