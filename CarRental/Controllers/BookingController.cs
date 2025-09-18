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
                PricePerDay = car.PricePerDay,
                PickUpDate = DateTime.Now,
                DropOffDate = DateTime.Now.AddDays(1),
                TotalAmount = car.PricePerDay
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Book(BookingViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingCustomerId = new Guid("3f29a7a2-5d1c-4e90-b1c3-9a7f8e4a1d2f");

            var booking = new Booking
            {
                CarId = model.CarId,
                CustomerId = existingCustomerId,  // <-- put it here
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
            // Server-side validation
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            // Additional server-side validation for card
            if (vm.Method == "CreditDebit")
            {
                if (string.IsNullOrWhiteSpace(vm.CardNumber) || vm.CardNumber.Length != 16)
                {
                    ModelState.AddModelError("CardNumber", "Card number must be exactly 16 digits.");
                    return View(vm);
                }

                if (string.IsNullOrWhiteSpace(vm.CVV) || vm.CVV.Length != 3)
                {
                    ModelState.AddModelError("CVV", "CVV must be exactly 3 digits.");
                    return View(vm);
                }
            }

            // Additional server-side validation for PayPal
            if (vm.Method == "PayPal")
            {
                if (string.IsNullOrWhiteSpace(vm.PayPalEmail) || !new EmailAddressAttribute().IsValid(vm.PayPalEmail))
                {
                    ModelState.AddModelError("PayPalEmail", "Enter a valid PayPal email.");
                    return View(vm);
                }
            }

            // Create Payment object
            var payment = new Payment
            {
                BookingId = vm.BookingId,
                Method = vm.Method,
                CardHolder = vm.CardHolder,
                CardNumber = vm.CardNumber,
                CVV = vm.CVV,
                PayPalEmail = vm.PayPalEmail
            };

            bookingService.AddPayment(payment);

            // Redirect to Confirmation with success message
            TempData["Success"] = "Payment Successful!";
            return RedirectToAction("Confirmation", new { bookingId = vm.BookingId });
        }

        public IActionResult Confirmation(Guid bookingId)
        {
            var booking = bookingService.GetById(bookingId);
            ViewBag.Message = TempData["Success"];
            return View(booking);
        }
    }
}
