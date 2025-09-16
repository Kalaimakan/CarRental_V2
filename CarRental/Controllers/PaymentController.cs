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

        public async Task<IActionResult> ViewPayment()
        {
            var payment = await _service.GetAllPaymentsAsync();
            return View(payment);
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
