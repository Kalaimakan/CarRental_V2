using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class CustomerDashboardController : Controller
    {
        // Main dashboard page
        public IActionResult Index()
        {
            // This will load: Views/CustomerDashboard/Index.cshtml
            return View();
        }

        // Browse cars page
        public IActionResult BrowseCars()
        {
            return View();
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
