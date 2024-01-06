using PoS.Abstractions;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Entities;

namespace PoS.Data.Repositories
{
    public class TaxRepository : Repository<Tax>, ITaxRepository
    {
        public TaxRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
