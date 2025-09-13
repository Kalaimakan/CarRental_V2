using CarRental.DTOs;

namespace CarRental.Interfaces
{
    public interface IPaymentService
    {
        Task AddPaymentAsync(PaymentDto paymentDto);
    }
}
