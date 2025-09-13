using CarRental.Interfaces;
using CarRental.DTOs;
using Microsoft.AspNetCore.Mvc;
using CarRental.Services;

namespace CarRental.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult AddPayment()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PaymentDto paymentDto)
        {
            if (ModelState.IsValid)
            {
                await _service.AddPaymentAsync(paymentDto);
                return RedirectToAction("Index", "Booking");
            }

            return View(paymentDto);
        }

    }
}
