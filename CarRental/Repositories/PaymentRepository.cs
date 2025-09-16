using CarRental.Database;
using CarRental.Interfaces;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarRental.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbcontext _Dbcontext;

        public PaymentRepository(AppDbcontext Dbcontext)
        {
            _Dbcontext = Dbcontext;
        }

        public async Task<List<Payment>> GetAllPaymentsAsync()
        {
            return await _Dbcontext.Payments.ToListAsync();
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _Dbcontext.Payments.AddAsync(payment);
            await _Dbcontext.SaveChangesAsync();
        }

       
    }

}


