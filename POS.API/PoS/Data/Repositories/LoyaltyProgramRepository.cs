using PoS.Abstractions;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Entities;

namespace PoS.Data.Repositories
{
    public class LoyaltyProgramRepository : Repository<LoyaltyProgram>, ILoyaltyProgramRepository
    {
        public LoyaltyProgramRepository(IApplicationDbContext context) : base(context)
        {

        }
    }
}
