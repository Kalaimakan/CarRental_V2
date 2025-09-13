using CarRental.Database;
using CarRental.Interfaces;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarRental.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbcontext _db;
        public BookingRepository(AppDbcontext db) => _db = db;

        public void Add(Booking booking)
        {
            _db.Bookings.Add(booking);
            _db.SaveChanges();
        }

        public void Update(Booking booking)
        {
            _db.Bookings.Update(booking);
            _db.SaveChanges();
        }

        public void Delete(Booking booking)
        {
            _db.Bookings.Remove(booking);
            _db.SaveChanges();
        }

        public Booking GetById(Guid id) =>
            _db.Bookings.Include(b => b.Car).Include(b => b.Customer).FirstOrDefault(b => b.Id == id);

        public List<Booking> GetAll() =>
            _db.Bookings.Include(b => b.Car).Include(b => b.Customer).ToList();
    }
}
