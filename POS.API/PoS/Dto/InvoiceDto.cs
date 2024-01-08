using PoS.Entities;

namespace PoS.Dto
{
    public class InvoiceDto
    {
        public InvoiceDto(Order order, IEnumerable<ItemCountPriceDto> items, IEnumerable<Tax> taxes, IEnumerable<Tip> tips, IEnumerable<LoyaltyProgram> loyaltyPrograms)
        {
            Order = order;
            Items = items;
            Taxes = taxes;
            Tips = tips;
            LoyaltyPrograms = loyaltyPrograms;
        }

        public Order Order { get; set; }
        public IEnumerable<ItemCountPriceDto> Items { get; set; }
        public IEnumerable<Tax> Taxes { get; set; }
        public IEnumerable<Tip> Tips { get; set; }
        public IEnumerable<LoyaltyProgram> LoyaltyPrograms { get; set; }
    }
}
