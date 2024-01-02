using PoS.Entities;
using PoS.Enums;

namespace PoS.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public Guid UserId { get; set; }
    }
}
