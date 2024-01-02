using PoS.Enums;
using System.Text.Json.Serialization;

namespace PoS.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public Guid UserId { get; set; }
        
        [JsonIgnore]
        public User? User { get; set; }
    }
}
