using CarRental.Models;

namespace CarRental.Interfaces
{
    public interface IBookingService
    {

        Booking GetById(Guid id);
        Booking AddBooking(Booking booking);
        void AddPayment(Payment payment);
    }
}
