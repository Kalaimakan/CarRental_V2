using CarRental.Models;

namespace CarRental.Interfaces
{
    public interface IBookingRepository
    {
        void Add(Booking booking);
        void Update(Booking booking);
        void Delete(Booking booking);
        Booking GetById(Guid id);
        List<Booking> GetAll();
    }
}
