using PoS.Abstractions;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Entities;


namespace PoS.Data.Repositories
{
    public class TaxOrderRepository : Repository<TaxOrder>, ITaxOrderRepository
    {
        public TaxOrderRepository(IApplicationDbContext context) : base(context) { }
    }
}
