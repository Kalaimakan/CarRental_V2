using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Database
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {
        }

        public DbSet<Customer> customers { get; set; }
    }
}
