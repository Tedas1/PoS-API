

namespace PoS.Entities
{
    public class Tip
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
