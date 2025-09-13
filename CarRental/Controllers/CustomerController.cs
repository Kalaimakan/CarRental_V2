using CarRental.DTOs;
using CarRental.Interfaces;
using CarRental.Repositories.Implementations;
using CarRental.Services.Implementations;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        public IActionResult RegisterCustomer()
        {
            return View();
        }

        public async Task<IActionResult> ViewCustomer()
        {
            var customer = await _service.GetAllCustomersAsync();
            return View(customer);
        }

       public IActionResult Add()
        {
            return View();
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
        public async Task <IActionResult> Add(CustomerDto dto)
        {
            if (ModelState.IsValid)
            {
                await _service.AddCustomerAsync(dto);
                TempData["SuccessMessage"] = $"{dto.Name} Added successfully!";
                return RedirectToAction("ViewCustomer");
            }
             return View(dto);
        }

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
