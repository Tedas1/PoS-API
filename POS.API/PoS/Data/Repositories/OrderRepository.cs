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
        private readonly ITaxRepository _taxRepository;
        private readonly ILoyaltyProgramRepository _loyaltyProgramRepository;
        private readonly ITipRepository _tipRepository;
        private readonly IItemRepository _itemRepository;

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
            _taxRepository = taxRepository;
            _loyaltyProgramRepository = loyaltyProgramRepository;
            _tipRepository = tipRepository;
            _itemRepository = itemRepository;
        }



        public async Task<InvoiceDto> GetOrderInvoiceAsync(Order order)
        {
            decimal totalSum = 0;


            // Items and their sums
            IEnumerable<ItemOrder> itemOrders = await _itemOrderRepository.GetMany(x => x.OrderId == order.Id);

            List<ItemCountPriceDto> items = new List<ItemCountPriceDto>();
            foreach (ItemOrder itemOrder in itemOrders)
            {
                Item item = await _itemRepository.Get(x => x.Id == itemOrder.ItemId) ?? new Item();
                items.Add(new ItemCountPriceDto(item.Id, item.PPU, itemOrder.Quantity));
                totalSum += itemOrder.Quantity * item.PPU;
            }
            
            // Taxes and their sums
            IEnumerable<TaxOrder> orderTaxes = await _taxOrderRepository.GetMany(x => x.OrderId == order.Id);

            List<Tax> taxes = new List<Tax>();
            decimal taxesPercentSum = 0;
            foreach (TaxOrder taxOrder in orderTaxes)
            {
                Tax tax = await _taxRepository.Get(x => x.TaxId == taxOrder.TaxId) ?? new Tax();
                taxes.Add(tax);
                taxesPercentSum += tax.Percentage;
            }

            totalSum += taxesPercentSum * totalSum / 100;

            // Loyalty Programs and their sums
            var loyaltyProgram = await _loyaltyProgramRepository.Get(x => x.UserId == order.UserId);

            if (loyaltyProgram != null)
            {
                if (loyaltyProgram.PointsAcquired > totalSum)
                {
                    totalSum = 0;
                }
                else
                {
                    totalSum -= loyaltyProgram.PointsAcquired;
                }
            }

            // Tips and their sums
            IEnumerable<Tip> orderTips = await _tipRepository.GetMany(x => x.OrderId == order.Id);

            var tipSum = orderTips.Sum(x => x.Amount);

            totalSum += tipSum;


            order.TotalAmount = Math.Round(totalSum, 2);
            

            return new InvoiceDto(order, items, taxes, orderTips, loyaltyProgram);
        }
    }
}
