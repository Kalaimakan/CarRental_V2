using CarRental.Interfaces;
using CarRental.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class CustomerDashboardController : Controller
    {
        private ICarService carService;
        public CustomerDashboardController(ICarService service) => this.carService = service;

        [Authorize]
        public IActionResult Index()
        {
            var customerIdClaim = User.FindFirst("CustomerId");
            var customerId = customerIdClaim != null ? customerIdClaim.Value : null;
            var customerName = User.Identity?.Name;

            ViewBag.CustomerName = customerName;
            ViewBag.CustomerId = customerId;
            
            return View();
        }

        // Browse cars page
        public IActionResult ShowAllCars()
        {
            var cars = carService.GetAllCars(); // Make sure this returns a List<CarDto>

            if (cars == null)
                cars = new List<CarRental.DTOs.CarDto>(); // prevent null reference

            return View(cars);
        }

        // My bookings page
        public IActionResult MyBookings()
        { 
            return View();
        }

        // Feedback page
        public IActionResult Feedback()
        { 
            return View();
        }

        // Help & FAQ page
        public IActionResult Help()
        {
            return View();
        }
    }
}
