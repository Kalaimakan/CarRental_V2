using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CarRental.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult OverView()
        {
            return View();
        }
    }
}