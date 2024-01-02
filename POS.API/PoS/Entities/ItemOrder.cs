namespace PoS.Entities
{
    public class ItemOrder
    {
        public Guid ItemId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }

        public ItemOrder(Guid itemId, Guid orderId, int quantity = 0)
        {
            ItemId = itemId;
            OrderId = orderId;
            Quantity = quantity;
        }
    }
}
