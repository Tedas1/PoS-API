using PoS.Enums;
using System.Text.Json.Serialization;

namespace PoS.Entities
{
    public class User
    {
        public User()
        {
            Orders = new List<Order>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public UserRole Role { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }

}
