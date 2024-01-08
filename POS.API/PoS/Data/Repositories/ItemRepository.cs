using PoS.Abstractions;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Dto;
using PoS.Entities;

namespace PoS.Data.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        private readonly IItemOrderRepository _itemOrderRepository;

        public ItemRepository(
            IApplicationDbContext context,
            IItemOrderRepository itemOrderRepository
            ) : base(context)
        {
            _itemOrderRepository = itemOrderRepository;
        }

        public async Task<bool> AssignItemToOrder(ItemQuantityDto itemQuantity, Guid orderId)
        {
            Item? item = await Get(x => x.Id == itemQuantity.ItemId);
            if (item == null || item.Stock < itemQuantity.Quantity) return false;
            
            var itemOrder = await _itemOrderRepository.Get(x => x.ItemId == item.Id && x.OrderId == orderId);
            if (itemOrder == null)
            {
                await _itemOrderRepository.Create(new ItemOrder(item.Id, orderId, itemQuantity.Quantity));
            }
            else
            {
                itemOrder.Quantity += itemQuantity.Quantity;
                _itemOrderRepository.Update(itemOrder);
            }

            item.Stock -= itemQuantity.Quantity;
            Update(item);

            await Save();
            await _itemOrderRepository.Save();

            return true;
        }
    }
}
