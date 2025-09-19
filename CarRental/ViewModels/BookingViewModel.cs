using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class BookingViewModel
    {

        public Guid CarId { get; set; }
        public string CarModel { get; set; }
        public string CarBrand { get; set; }
        public string CarImage { get; set; }
        public decimal PricePerDay { get; set; }

        [Required(ErrorMessage = "Pick-up date is required")]
        public DateTime PickUpDate { get; set; }

        [Required(ErrorMessage = "Drop-off date is required")]
        [CustomValidation(typeof(BookingViewModel), nameof(ValidateDropOffDate))]
        public DateTime DropOffDate { get; set; }

        public decimal TotalAmount { get; set; }

        // 🔹 Custom Validation Method
        public static ValidationResult ValidateDropOffDate(DateTime dropOff, ValidationContext context)
        {
            var instance = (BookingViewModel)context.ObjectInstance;
            if (dropOff <= instance.PickUpDate.AddHours(24))
            {
                return new ValidationResult("Drop-off date must be at least 24 hours after Pick-up date");
            }
            return ValidationResult.Success;
        }
    }
}
