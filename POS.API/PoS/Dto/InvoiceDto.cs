using PoS.Entities;

namespace PoS.Dto
{
    public class InvoiceDto
    {
        public InvoiceDto(Order order, IEnumerable<ItemCountPriceDto> items, IEnumerable<Tax> taxes, IEnumerable<Tip> tips, LoyaltyProgram? loyaltyProgram)
        {
            Order = order;
            Items = items;
            Taxes = taxes;
            Tips = tips;
            LoyaltyProgram = loyaltyProgram;
        }

        public Order Order { get; set; }
        public IEnumerable<ItemCountPriceDto> Items { get; set; }
        public IEnumerable<Tax> Taxes { get; set; }
        public IEnumerable<Tip> Tips { get; set; }
        public LoyaltyProgram? LoyaltyProgram { get; set; }
    }
}
