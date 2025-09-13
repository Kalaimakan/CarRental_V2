using CarRental.Database;
using CarRental.Interfaces;
using CarRental.Repositories;
using CarRental.Repositories.Implementations;
using CarRental.Services;
using CarRental.Services.Implementations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CarRental
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbcontext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("CarRentalConnection")));

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();

            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();

            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "Default",
                pattern: "{controller=AdminDashboard}/{action=AdminDashboard}/{id?}");

            app.Run();
        }
    }
}
