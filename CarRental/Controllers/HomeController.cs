using CarRental.Interfaces;
using CarRental.Models;
using CarRental.Services;   // ✅ Add this namespace for ICarService
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarRental.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarService _carService;

        // ✅ Only one constructor with both dependencies
        public HomeController(ILogger<HomeController> logger, ICarService carService)
        {
            _logger = logger;
            _carService = carService;
        }

        // ✅ Show all cars in home page
        public IActionResult Index()
        {

            var cars = _carService.GetAllCars(); // from Service -> Repository -> DB
            return View(cars); // pass to view (List<CarDto>)
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
