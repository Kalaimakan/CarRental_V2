using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }

        // 🔗 Foreign Key to Car
        [Required]
        [ForeignKey("Car")]
        public Guid CarId { get; set; }
        public Car Car { get; set; }

        // 🔗 Foreign Key to Customer
        [Required]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = "Pending"; // Pending / Co
    }
}
