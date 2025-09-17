using CarRental.DTOs;

namespace CarRental.Interfaces
{
    public interface IPaymentService
    {
        Task<List<PaymentDto>> GetAllPaymentsAsync();
        Task AddPaymentAsync(PaymentDto paymentDto);
    }
}
