namespace CarRental.DTOs
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }

        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
