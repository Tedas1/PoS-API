using Newtonsoft.Json;
using PoS.Enums;

namespace PoS.Entities
{
    public class Order
    {
        public Order()
        {
            User = new User();
            Items = new List<Item>();
        }

        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public Guid UserId { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Item> Items { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
