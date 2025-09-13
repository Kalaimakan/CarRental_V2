using CarRental.Interfaces;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _service;

        public BookingController(IBookingService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var bookings = _service.GetAllBookings();
            return View(bookings);
        }

        [HttpGet]
        public IActionResult Add(Guid carId, Guid customerId)
        {
            return View(new BookingViewModel { CarId = carId, CustomerId = customerId });
        }

        [HttpPost]
        public IActionResult Add(BookingViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _service.CreateBooking(model);
            TempData["Success"] = "Booking created successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var booking = _service.GetBooking(id);
            if (booking == null) return NotFound();

            return View(booking);
        }

        [HttpGet]
        public IActionResult Cancel(Guid id)
        {
            _service.UpdateBookingStatus(id, "Cancelled");
            return RedirectToAction("Index");
        }
    }
}
