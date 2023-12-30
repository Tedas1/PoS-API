using PoS.Dto;
using PoS.Entities;

namespace PoS.Abstractions.Repositories.EntityRepositories
{
    public interface IItemRepository: IRepository<Item>
    {
        Task<bool> AssignItemToOrder(ItemQuantityDto itemQuantity, Guid orderId);
    }
}
