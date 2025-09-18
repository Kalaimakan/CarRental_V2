using CarRental.Models;

namespace CarRental.Interfaces
{
    public interface IBookingRepository
    {
        void AddBooking(Booking booking);
        Booking GetById(Guid id);
    }
}
