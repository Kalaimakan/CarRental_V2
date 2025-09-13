namespace CarRental.ViewModels
{
    public class PaymentViewModel
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // e.g., Credit Card, PayPal
        public DateTime PaymentDate { get; set; }

    }
}
