using PoS.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid EmployeeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date {  get; set; }

        public int TimeSlot { get; set; }
        public Guid? OrderId { get; set; }
        public ReservationStatus Status { get; set; }
    }
}
