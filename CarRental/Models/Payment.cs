using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Booking")]
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; }

        public decimal Amount { get; set; } 
        
        public string PaymentMethod { get; set; } // e.g., Credit Card, PayPal
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        
       
    }
}
