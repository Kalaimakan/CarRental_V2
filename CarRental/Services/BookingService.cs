using CarRental.Database;
using CarRental.Interfaces;
using CarRental.Models;

namespace CarRental.Services
{
    public class BookingService: IBookingService
    {
        private readonly IBookingRepository repo;
        private readonly AppDbcontext db;

        public BookingService(IBookingRepository repo, AppDbcontext db)
        {
            this.repo = repo;
            this.db = db;
        }

        public Booking AddBooking(Booking booking)
        {
            booking.Id = Guid.NewGuid();
            repo.AddBooking(booking);
            return booking;
        }

        public Booking GetById(Guid id) => repo.GetById(id);

        public void AddPayment(Payment payment)
        {
            payment.Id = Guid.NewGuid();
            payment.PaidOn = DateTime.Now;
            db.Payments.Add(payment);
            db.SaveChanges();
        }
    }
}
