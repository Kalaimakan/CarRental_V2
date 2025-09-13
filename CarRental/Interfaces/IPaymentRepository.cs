using CarRental.Models;

namespace CarRental.Interfaces
{
    public interface IPaymentRepository
    {
        Task AddPaymentAsync(Payment payment);
    }
}
