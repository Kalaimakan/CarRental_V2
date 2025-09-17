using CarRental.DTOs;
using CarRental.Interfaces;
using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;

        public BookingService(IBookingRepository repo)
        {
            _repo = repo;
        }

        public void CreateBooking(BookingViewModel model)
        {
            var days = (model.EndDate - model.StartDate).Days;
            if (days <= 0) throw new Exception("End date must be after start date.");

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                CarId = model.CarId,
                CustomerId = model.CustomerId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                TotalPrice = days * 1000, // TODO: fetch Car.PricePerDay dynamically
                Status = "Pending"
            };

            _repo.Add(booking);
        }

        public List<BookingDto> GetAllBookings()
        {
            return _repo.GetAll().Select(b => new BookingDto
            {
                Id = b.Id,
                CarId = b.CarId,
                CarBrand = b.Car.CarBrand,
                CarModel = b.Car.CarModel,
                CustomerId = b.CustomerId,
                CustomerName = b.Customer.Name,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                TotalPrice = b.TotalPrice,
                Status = b.Status
            }).ToList();
        }

        public BookingDto GetBooking(Guid id)
        {
            var b = _repo.GetById(id);
            if (b == null) return null;

            return new BookingDto
            {
                Id = b.Id,
                CarId = b.CarId,
                CarBrand = b.Car.CarBrand,
                CarModel = b.Car.CarModel,
                CustomerId = b.CustomerId,
                CustomerName = b.Customer.Name,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                TotalPrice = b.TotalPrice,
                Status = b.Status
            };
        }

        public void UpdateBookingStatus(Guid id, string status)
        {
            var b = _repo.GetById(id);
            if (b == null) return;

            b.Status = status;
            _repo.Update(b);
        }

        public void DeleteBooking(Guid id)
        {
            var b = _repo.GetById(id);
            if (b != null)
                _repo.Delete(b);
        }
    }
}