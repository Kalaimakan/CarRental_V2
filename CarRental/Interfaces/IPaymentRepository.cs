using CarRental.Models;

namespace CarRental.Interfaces
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllPaymentsAsync();
        Task AddPaymentAsync(Payment payment);
    }
}
