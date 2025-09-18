using CarRental.Database;
using CarRental.Interfaces;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Repositories
{
    public class BookingRepository: IBookingRepository
    {
        private readonly AppDbcontext db;
        public BookingRepository(AppDbcontext db)
        {
            this.db = db;
        }

        public void AddBooking(Booking booking)
        {
            db.Bookings.Add(booking);
            db.SaveChanges();
        }

        public Booking GetById(Guid id)
        {
            return db.Bookings
                .Include(b => b.Car)
                .Include(b => b.Payment)
                .Include(b => b.Customer)
                .FirstOrDefault(b => b.Id == id);
        }
    }
}
