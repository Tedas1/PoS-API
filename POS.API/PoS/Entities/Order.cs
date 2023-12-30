using Newtonsoft.Json;
using PoS.Enums;

namespace PoS.Entities
{
    public class Order
    {
        public Order()
        {
            User = new User();
        }

        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public Guid UserId { get; set; }
        
        [JsonIgnore]
        public User User { get; set; }
    }
}
