using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class CustomerDashboardController : Controller
    {
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
