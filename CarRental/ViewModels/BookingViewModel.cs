using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class BookingViewModel
    {
        public Guid CarId { get; set; }
        public Guid CustomerId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
