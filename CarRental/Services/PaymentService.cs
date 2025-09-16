using CarRental.DTOs;
using CarRental.Interfaces;
using CarRental.Models;
using CarRental.Repositories;

namespace CarRental.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;

        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _repository.GetAllPaymentsAsync();
            return payments.Select(p => new PaymentDto
            {
                Id = p.Id,
                BookingId = p.BookingId,
                Amount = p.Amount,
                PaymentMethod = p.PaymentMethod,
                PaymentDate = p.PaymentDate,
               
            }).ToList();
        }

        public async Task AddPaymentAsync(PaymentDto paymentDto)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                BookingId = paymentDto.BookingId,
                Amount = paymentDto.Amount,
                PaymentMethod = paymentDto.PaymentMethod,
                PaymentDate = DateTime.UtcNow,
                
               
            };

            await _repository.AddPaymentAsync(payment);
        }

    }

}

