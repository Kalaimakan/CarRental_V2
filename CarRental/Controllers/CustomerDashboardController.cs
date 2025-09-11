using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class CustomerDashboardController : Controller
    {
        public IActionResult Index()
        {
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
