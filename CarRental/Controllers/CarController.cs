using CarRental.DTOs;
using CarRental.Interfaces;
using CarRental.Models;
using CarRental.Services;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService service;
        public CarController(ICarService service) => this.service = service;

        [HttpGet]
        public IActionResult ViewCar()
        {
            var cars = service.GetAllCars();
            return View(cars);
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(CarViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            service.AddCar(model);
            TempData["Success"] = "Car added successfully!";
            return RedirectToAction(nameof(ViewCar));
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var dto = service.GetCarById(id);
            if (dto == null) return NotFound();

            var model = new CarViewModel
            {
                Id = dto.Id,
                CarBrand = dto.CarBrand,
                CarModel = dto.CarModel,
                CarColour = dto.CarColour,
                Seats = dto.Seats,
                FuelType = dto.FuelType,
                Transmission = dto.Transmission,
                HasAC = dto.HasAC,
                PricePerDay = dto.PricePerDay,
                Status = dto.Status,
                Description = dto.Description,
                ExistingImages = dto.Images
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(CarViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            service.UpdateCar(model);
            TempData["Success"] = "Car updated successfully!";
            return RedirectToAction(nameof(ViewCar));
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            service.DeleteCar(id);
            TempData["Success"] = "Car deleted successfully!";
            return RedirectToAction(nameof(ViewCar));
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var car = service.GetCarById(id); // Service → Repo

            if (car == null) return NotFound();

            return View(car);

        }
    }

}
