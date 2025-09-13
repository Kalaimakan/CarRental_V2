using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Database
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {
        }

        public DbSet<Car> cars { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
