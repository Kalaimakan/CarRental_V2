using CarRental.Interfaces;
using CarRental.Models;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService bookingService;
        private readonly ICarService carService;

        public BookingController(IBookingService bookingService, ICarService carService)
        {
            this.bookingService = bookingService;
            this.carService = carService;
        }

        [HttpGet]
        public IActionResult Book(Guid carId)
        {
            var car = carService.GetCarById(carId);
            var vm = new BookingViewModel
            {
                CarId = car.Id,
                CarModel = car.CarModel,
                CarBrand = car.CarBrand,
                CarImage = car.Images.FirstOrDefault()?.FileName,
                PickUpDate = DateTime.Now,
                DropOffDate = DateTime.Now.AddDays(1),
                TotalAmount = car.PricePerDay
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Book(BookingViewModel model)
        {
            var booking = new Booking
            {
                CarId = model.CarId,
                PickUpDate = model.PickUpDate,
                DropOffDate = model.DropOffDate,
                TotalAmount = model.TotalAmount
            };

            var savedBooking = bookingService.AddBooking(booking);
            return RedirectToAction("Payment", new { bookingId = savedBooking.Id });
        }

        [HttpGet]
        public IActionResult Payment(Guid bookingId)
        {
            var booking = bookingService.GetById(bookingId);
            var vm = new PaymentViewModel
            {
                BookingId = booking.Id,
                CarModel = booking.Car.CarModel,
                Dates = $"{booking.PickUpDate:dd MMM} - {booking.DropOffDate:dd MMM}",
                Total = booking.TotalAmount
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Payment(PaymentViewModel vm)
        {
            var payment = new Payment
            {
                BookingId = vm.BookingId,
                Method = vm.Method,
                CardHolder = vm.CardHolder,
                CardNumber = vm.CardNumber,
                CVV = vm.CVV
            };

            bookingService.AddPayment(payment);
            TempData["Success"] = "Payment Successful!";
            return RedirectToAction("Confirmation", new { bookingId = vm.BookingId });
        }

        public IActionResult Confirmation(Guid bookingId)
        {
            var booking = bookingService.GetById(bookingId);
            return View(booking);
        }
    }
}
