using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public decimal TotalAmount { get; set; }

        public Car Car { get; set; }
        public Payment Payment { get; set; }
    }
}
