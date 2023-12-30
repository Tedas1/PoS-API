using PoS.Abstractions;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Entities;

namespace PoS.Data.Repositories
{
    public class ItemOrderRepository : Repository<ItemOrder>, IItemOrderRepository
    {
        public ItemOrderRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
