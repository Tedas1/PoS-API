using PoS.Abstractions;
using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Dto;
using PoS.Entities;

namespace PoS.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IItemOrderRepository _itemOrderRepository;
        private readonly ITaxOrderRepository _taxOrderRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ITaxRepository _taxRepository;
        private readonly ILoyaltyProgramRepository _loyaltyProgramRepository;
        private readonly ITipRepository _tipRepository;

        public OrderRepository(
            IApplicationDbContext context,
            IItemOrderRepository itemOrderRepository,
            ITaxOrderRepository taxOrderRepository,
            IItemRepository itemRepository,
            ITaxRepository taxRepository,
            ILoyaltyProgramRepository loyaltyProgramRepository,
            ITipRepository tipRepository
            ) : base(context)
        {
            _itemOrderRepository = itemOrderRepository;
            _taxOrderRepository = taxOrderRepository;
            _itemRepository = itemRepository;
            _taxRepository = taxRepository;
            _loyaltyProgramRepository = loyaltyProgramRepository;
            _tipRepository = tipRepository;
        }
        
        
        
        public async Task<InvoiceDto> GetOrderInvoiceAsync(Order order)
        {
            decimal totalSum = 0;


            // Items and their sums
            IEnumerable<ItemOrder> itemOrders = await _itemOrderRepository.GetMany(x => x.OrderId == order.Id);

            List<ItemCountPriceDto> items = new List<ItemCountPriceDto>();
            foreach(ItemOrder itemOrder in itemOrders)
            {
                Item item = await _itemRepository.Get(x => x.Id == itemOrder.ItemId) ?? new Item();
                items.Add(new ItemCountPriceDto(item.Id, item.PPU, itemOrder.Quantity));
                totalSum += itemOrder.Quantity * item.PPU;
            }

            // Tips and their sums
            IEnumerable<Tip> orderTips = await _tipRepository.GetMany(x => x.OrderId == order.Id);
            
            var tipSum = orderTips.Sum(x => x.Amount);

            totalSum += tipSum;
            
            // Loyalty Programs and their sums
            IEnumerable<LoyaltyProgram> loyaltyPrograms = await _loyaltyProgramRepository.GetMany(x => x.UserId == order.UserId);

            var loyaltySum = loyaltyPrograms.Sum(x => x.PointsAcquired);
            totalSum += loyaltySum;
            

            // Taxes and their sums
            IEnumerable<TaxOrder> orderTaxes = await _taxOrderRepository.GetMany(x => x.OrderId == order.Id);

            List<Tax> taxes = new List<Tax>();
            foreach(TaxOrder taxOrder in orderTaxes)
            {
                Tax tax = await _taxRepository.Get(x => x.TaxId == taxOrder.TaxId) ?? new Tax();
                taxes.Add(tax);
                totalSum -= (tax.Percentage / 100) * totalSum;
            }

            order.TotalAmount = totalSum;

            return new InvoiceDto(order, items, taxes, orderTips, loyaltyPrograms);
        }
    }
}
