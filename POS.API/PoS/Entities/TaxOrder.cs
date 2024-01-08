namespace PoS.Entities
{
    public class TaxOrder
    {
        public Guid TaxId { get; set; }
        public Guid OrderId { get; set; }
        public TaxOrder(Guid taxId, Guid orderId)
        {
            TaxId = taxId;
            OrderId = orderId;
        }
    }

}
