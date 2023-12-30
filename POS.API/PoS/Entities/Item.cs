using Newtonsoft.Json;
using PoS.Enums;

namespace PoS.Entities
{
    public class Item
    {
        public Item()
        {
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ItemType Type { get; set; }
        public decimal PPU { get; set; } // price per unit
        public int Stock { get; set; }
        
    }
}
