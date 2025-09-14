using CarRental.Database;
using CarRental.Interfaces;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;

namespace CarRental.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbcontext _db;
        public CarRepository(AppDbcontext db) { _db = db; }

        public void Add(Car car)
        {
            _db.cars.Add(car);
            _db.SaveChanges();
        }

        public void Update(Car car)
        {
            try
            {
                _db.cars.Update(car);
                _db.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine("❌ Database Update Error: " + dbEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Unexpected Error: " + ex.Message);
            }
        }

        public void Delete(Car car)
        {
            _db.cars.Remove(car);
            _db.SaveChanges();
        }

        public Car GetById(Guid id) =>
            _db.cars.Include(c => c.Images).FirstOrDefault(c => c.Id == id);

        public List<Car> GetAll() =>
            _db.cars.Include(c => c.Images).ToList();


        //public async Task<List<Car>> SearchCarsAsync(string searchTerm)
        //{
        //    if (string.IsNullOrWhiteSpace(searchTerm))
        //        return await _db.cars.ToListAsync();

        //    searchTerm = searchTerm.ToLower();
        //    if (int.TryParse(searchTerm, out int pricePerDay))

        //        return await _db.cars
        //        .Where(c => c.CarModel.ToLower().Contains(searchTerm) ||
        //                    c.CarBrand.ToLower().Contains(searchTerm) ||
        //                    c.PricePerDay == pricePerDay)

        //        .ToListAsync();

        //    return await _db.cars
        //.Where(c => c.CarModel.ToLower().Contains(searchTerm) ||
        //            c.CarBrand.ToLower().Contains(searchTerm))
        //.ToListAsync();


        //}

        //public Task<List<Car>> GetAllCarsAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}


