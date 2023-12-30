using Newtonsoft.Json;
using PoS.Enums;

namespace PoS.Entities
{
    public class Item
    {
        public Item()
        {
            Orders = new List<Order>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ItemType Type { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
