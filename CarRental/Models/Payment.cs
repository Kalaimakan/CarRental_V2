using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }

        public string Method { get; set; }   // CreditDebit, PayPal, Cash
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string PayPalEmail { get; internal set; }
        public DateTime PaidOn { get; set; }

        public Booking Booking { get; set; }
       
    }
}
