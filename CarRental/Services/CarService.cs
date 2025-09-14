using CarRental.DTOs;
using CarRental.Interfaces;
using CarRental.Models;
using CarRental.Repositories;
using CarRental.ViewModels;
using Microsoft.VisualBasic.FileIO;
using System.Security.Cryptography;

namespace CarRental.Services;
public class CarService : ICarService
    {
    private readonly ICarRepository repo;
    private readonly IWebHostEnvironment env;

    public CarService(ICarRepository repo, IWebHostEnvironment env)
    {
        this.repo = repo;
        this.env = env;
    }

    public void AddCar(CarViewModel model)
    {
        var car = new Car
        {
            Id = Guid.NewGuid(),
            CarBrand = model.CarBrand,
            CarModel = model.CarModel,
            CarColour = model.CarColour,
            Seats = model.Seats,
            FuelType = model.FuelType,
            Transmission = model.Transmission,
            HasAC = model.HasAC,
            PricePerDay = model.PricePerDay,
            Status = model.Status,
            Description = model.Description,
            Images = new List<CarImage>()
        };

        var imgFolder = Path.Combine(env.WebRootPath, "images");
        if (!Directory.Exists(imgFolder))
            Directory.CreateDirectory(imgFolder);

        if (model.ImageFiles != null && model.ImageFiles.Any())
        {
            foreach (var file in model.ImageFiles)
            {
                if (file.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var path = Path.Combine(imgFolder, fileName);

                    using var stream = new FileStream(path, FileMode.Create);
                    file.CopyTo(stream);

                    car.Images.Add(new CarImage
                    {
                        Id = Guid.NewGuid(),
                        FileName = fileName,
                        CarId = car.Id
                    });
                }
            }
        }

        repo.Add(car);

    }

    public void UpdateCar(CarViewModel model)
    {
        var car = repo.GetById(model.Id);
        if (car == null) return;

        car.CarBrand = model.CarBrand;
        car.CarModel = model.CarModel;
        car.CarColour = model.CarColour;
        car.Seats = model.Seats;
        car.FuelType = model.FuelType;
        car.Transmission = model.Transmission;
        car.HasAC = model.HasAC;
        car.PricePerDay = model.PricePerDay;
        car.Status = model.Status;
        car.Description = model.Description;

        if (model.ImageFiles != null && model.ImageFiles.Any())
        {
            foreach (var img in car.Images)
            {
                var path = Path.Combine(env.WebRootPath, "images", img.FileName);
                if (File.Exists(path)) File.Delete(path);
            }
            car.Images.Clear();
            SaveImages(model.ImageFiles, car);
        }

        repo.Update(car);
    }

    public void DeleteCar(Guid id)
    {
        var car = repo.GetById(id);
        if (car == null) return;

        foreach (var img in car.Images)
        {
            var path = Path.Combine(env.WebRootPath, "images", img.FileName);
            if (File.Exists(path)) File.Delete(path);
        }

        repo.Delete(car);
    }

    public List<CarDto> GetAllCars()
    {
        return repo.GetAll().Select(c => new CarDto
        {
            Id = c.Id,
            CarBrand = c.CarBrand,
            CarModel = c.CarModel,
            CarColour = c.CarColour,
            Seats = c.Seats,
            FuelType = c.FuelType,
            Transmission = c.Transmission,
            HasAC = c.HasAC,
            PricePerDay = c.PricePerDay,
            Status = c.Status,
            Description = c.Description,
            Images = c.Images.Select(i => new CarImageDto { Id = i.Id, FileName = i.FileName }).ToList()
        }).ToList();
    }

    public CarDto GetCarById(Guid id)
    {
        var c = repo.GetById(id);
        if (c == null) return null;
        return new CarDto
        {
            Id = c.Id,
            CarBrand = c.CarBrand,
            CarModel = c.CarModel,
            CarColour = c.CarColour,
            Seats = c.Seats,
            FuelType = c.FuelType,
            Transmission = c.Transmission,
            HasAC = c.HasAC,
            PricePerDay = c.PricePerDay,
            Status = c.Status,
            Description = c.Description,
            Images = c.Images.Select(i => new CarImageDto { Id = i.Id, FileName = i.FileName }).ToList()
        };
    }

    private void SaveImages(List<IFormFile> files, Car car)
    {
        if (files == null) return;
        foreach (var file in files)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var path = Path.Combine(env.WebRootPath, "images", fileName);
            using var stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);

            car.Images.Add(new CarImage { Id = Guid.NewGuid(), FileName = fileName, CarId = car.Id });
        }
    }

    //public async Task<List<CarDto>> SearchCarsAsync(string searchTerm)
    //{
    //    if (string.IsNullOrWhiteSpace(searchTerm))
    //        return await repo.GetAllCarsAsync(); 

    //    searchTerm = searchTerm.ToLower();

    //    // Attempt to parse PricePerDay if the searchTerm is numeric
    //    if (int.TryParse(searchTerm, out int pricePerDay))
    //    {
    //        // Filter cars by brand, model or price per day if searchTerm is numeric
    //        var cars = await repo.SearchCarsAsync(searchTerm, pricePerDay);

    //        return cars.Select(c => new CarDto
    //        {
              
    //            CarBrand = c.CarBrand,
    //            CarModel = c.CarModel,
    //            PricePerDay = c.PricePerDay
    //        }).ToList();
    //    }

    //    var filteredCars = await repo.SearchCarsAsync(searchTerm);

    //    return filteredCars.Select(c => new CarDto
    //    {
    //        Id = c.Id,
    //        CarBrand = c.CarBrand,
    //        CarModel = c.CarModel,
    //        PricePerDay = c.PricePerDay
    //    }).ToList();
    }
















