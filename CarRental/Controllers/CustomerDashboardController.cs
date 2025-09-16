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

        public IActionResult BrowseCars()
        {
            return View();
        }

        public IActionResult MyBookings()
        {
            return View();
        }

        public IActionResult Feedback()
        {
            return View();
        }

        public IActionResult HelpAndFAQ()
        {
            return View();
        }
    }
}
