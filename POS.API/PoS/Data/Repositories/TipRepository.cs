using PoS.Abstractions;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Entities;

namespace PoS.Data.Repositories
{
    public class TipRepository : Repository<Tip>, ITipRepository
    {
        public TipRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
