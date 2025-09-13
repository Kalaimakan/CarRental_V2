using CarRental.DTOs;
using CarRental.ViewModels;

namespace CarRental.Interfaces
{
    public interface IBookingService
    {
        void CreateBooking(BookingViewModel model);
        List<BookingDto> GetAllBookings();
        BookingDto GetBooking(Guid id);
        void UpdateBookingStatus(Guid id, string status);
        void DeleteBooking(Guid id);

    }
}
