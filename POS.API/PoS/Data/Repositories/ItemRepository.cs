﻿using PoS.Abstractions;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Dto;
using PoS.Entities;

namespace PoS.Data.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IItemOrderRepository _itemOrderRepository;

        public ItemRepository(
            IApplicationDbContext context,
            IOrderRepository orderRepository,
            IItemOrderRepository itemOrderRepository
            ) : base(context)
        {
            _orderRepository = orderRepository;
            _itemOrderRepository = itemOrderRepository;
        }

        public async Task<bool> AssignItemToOrder(ItemQuantityDto itemQuantity, Guid orderId)
        {
            Order? order = await _orderRepository.Get(x => x.Id == orderId);
            if (order == null) return false;
            
            Item? item = await Get(x => x.Id == itemQuantity.ItemId);
            if (item == null) return false;
           
            
            var itemOrder = await _itemOrderRepository.Get(x => x.ItemId == item.Id && x.OrderId == order.Id);
            if (itemOrder == null)
            {
                await _itemOrderRepository.Create(new ItemOrder(item.Id, order.Id, itemQuantity.Quantity));
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
