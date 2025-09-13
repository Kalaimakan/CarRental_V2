using CarRental.DTOs;
using CarRental.Interfaces;
using CarRental.Repositories.Implementations;
using CarRental.Services;
using CarRental.Services.Implementations;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CarRental.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;

        private readonly IOtpService _otpService;
        private readonly IEmailService _emailService;

        public CustomerController(ICustomerService customerService, IOtpService otpService, IEmailService emailService)
        {
            _service = customerService;
            _otpService = otpService;
            _emailService = emailService;
        }

        //testing
        [HttpGet]
        public IActionResult TestEmail()
        {
            try
            {
                _emailService.SendEmail("ut01482tic2024@gmail.com", "Test Email", "Hello, this is a test!");
                return Content("Email sent successfully!");
            }
            catch (Exception ex)
            {
                return Content("Email sending failed: " + ex.Message);
            }
        }

        public IActionResult RegisterCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(CustomerViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);

            var otp = _otpService.GenerateOtp(model.Email);

            try
            {
                _emailService.SendEmail(model.Email, "Welcome! Your OTP Code", $"Hello {model.Name}, your OTP is: {otp}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Could not send OTP email: " + ex.Message);
                return View(model);
            }

            TempData["RegisterData"] = JsonSerializer.Serialize(model);

            // Redirect to VerifyOtp page
            return RedirectToAction("VerifyOtpRegister", new { email = model.Email });
        }

        [HttpGet]
        public IActionResult VerifyOtpRegister(string email)
        {
            return View(new OtpViewModel { Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> VerifyOtpRegister(OtpViewModel model)
        {
            if (_otpService.VerifyOtp(model.Email, model.Otp))
            {
                var dtoJson = TempData["RegisterData"] as string;
                if (dtoJson != null)
                {
                    var registerData = JsonSerializer.Deserialize<CustomerViewModel>(dtoJson);

                    var dto = new CustomerDto
                    {
                        Name = registerData.Name,
                        Address = registerData.Address,
                        PhoneNumber = registerData.PhoneNumber,
                        LicenceNumber = registerData.LicenceNumber,
                        Email = registerData.Email,
                        UserName = registerData.Username,
                        Password = registerData.Password
                    };

                    await _service.AddCustomerAsync(dto);
                }

                return RedirectToAction("Success");
            }

            ModelState.AddModelError("", "Invalid OTP");
            return View(model);
        }

        public IActionResult Success()
        {
            return View();
        }

        public async Task<IActionResult> ViewCustomer()
        {
            var customer = await _service.GetAllCustomersAsync();
            return View(customer);
        }

        //public IActionResult Add()
        //{
        //    return View();
        //}
        //public async Task<IActionResult> Add(CustomerDto dto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _service.AddCustomerAsync(dto);
        //        TempData["SuccessMessage"] = $"{dto.Name} Added successfully!";
        //        return RedirectToAction("ViewCustomer");
        //    }
        //    return View(dto);
        //}

        //otp add
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerDto dto)
        {
            if (ModelState.IsValid)
            {
                // 1. Generate OTP for new customer
                var otp = _otpService.GenerateOtp(dto.Email);

                // 2. Send OTP email
                try
                {
                    _emailService.SendEmail(dto.Email, "Welcome! Your OTP Code", $"Hello {dto.Name}, your OTP is: {otp}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Customer added but OTP email could not be sent: " + ex.Message);
                }

                // 3. Save customer in the database
                await _service.AddCustomerAsync(dto);

                // 4. Success message and redirect to ViewCustomer
                TempData["SuccessMessage"] = $"{dto.Name} added successfully";
                return RedirectToAction("VerifyOtpAdmin", new { email = dto.Email });
            }

            return View(dto);
        }

        [HttpGet]
        public IActionResult VerifyOtpAdmin(string email)
        {
            return View(new OtpViewModel { Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> VerifyOtpAdmin(OtpViewModel model)
        {
            if (_otpService.VerifyOtp(model.Email, model.Otp))
            {
                var dtoJson = TempData["RegisterData"] as string;
                if (dtoJson != null)
                {
                    var registerData = JsonSerializer.Deserialize<CustomerViewModel>(dtoJson);

                    var dto = new CustomerDto
                    {
                        Name = registerData.Name,
                        Address = registerData.Address,
                        PhoneNumber = registerData.PhoneNumber,
                        LicenceNumber = registerData.LicenceNumber,
                        Email = registerData.Email,
                        UserName = registerData.Username,
                        Password = registerData.Password
                    };

                    await _service.AddCustomerAsync(dto);
                }

                return RedirectToAction("ViewCustomer");
            }

            ModelState.AddModelError("", "Invalid OTP");
            return View(model);
        }

        public async Task<IActionResult> CustomerDetail(Guid id)
        {
            var customer = await _service.GetCustomerIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var customer = await _service.GetCustomerIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        //[HttpPost]
        //public async Task<IActionResult> Update(CustomerDto dto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _service.UpdateCustomerAsync(dto);
        //        return RedirectToAction("ViewCustomer");
        //    }
        //    return View(dto);
        //}



        // Type 01 SuccessMessage ============

        [HttpPost]
        public async Task<IActionResult> Update(CustomerDto dto)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateCustomerAsync(dto);

                TempData["SuccessMessage"] = $"{dto.Name} updated successfully!";

                return RedirectToAction("ViewCustomer");
            }
            return View(dto);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await _service.GetCustomerIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            TempData["SuccessMessage"] = $"{customer.Name} Deleted successfully!";
            await _service.DeleteCustomerAsync(id);
            return RedirectToAction("ViewCustomer");
        }

        ////[HttpGet("search")]
        ////public async Task<IActionResult> SearchCustomers([FromQuery] string query)
        ////{
        ////    var results = await _service.SearchCustomersAsync(query);
        ////    return Ok(results);
        ////}

        //[HttpGet("search")]
        //public async Task<IActionResult> SearchCustomers([FromQuery] string query)
        //{
        //    if (string.IsNullOrWhiteSpace(query))
        //        return BadRequest("Search term cannot be empty.");

        //    var results = await _service.SearchCustomersAsync(query);
        //    return Ok(results);
        //}

        [HttpGet]
        public async Task<IActionResult> SearchCustomers([FromQuery] string query)
        {
            var results = await _service.SearchCustomersAsync(query ?? "");
            return Ok(results);
        }
    }
}