using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class OverviewController : Controller
    {
        public IActionResult Overview()
        {
            return View();
        }
    }
}
