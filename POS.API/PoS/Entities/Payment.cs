using PoS.Enums;

namespace PoS.Entities
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public PaymentType PaymentType { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal? Change { get; set; }
    }
}
