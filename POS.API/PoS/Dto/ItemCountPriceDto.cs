namespace PoS.Dto
{
    public class ItemCountPriceDto
    {
        public ItemCountPriceDto(Guid itemId, decimal pPU, int quantity)
        {
            ItemId = itemId;
            PPU = pPU;
            Quantity = quantity;
        }

        public Guid ItemId { get; set; }
        public decimal PPU { get; set; }
        public int Quantity { get; set; }
    }
}
