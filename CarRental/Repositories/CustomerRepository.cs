﻿using CarRental.Database;
using CarRental.Interfaces;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbcontext _Dbcontext;

        public CustomerRepository(AppDbcontext Dbcontext)
        {
            _Dbcontext = Dbcontext;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _Dbcontext.customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            return await _Dbcontext.customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            _Dbcontext.customers.Add(customer);
            await _Dbcontext.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _Dbcontext.customers.Update(customer);
            await _Dbcontext.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            _Dbcontext.customers.Remove(customer);
            await _Dbcontext.SaveChangesAsync();
        }

        public async Task<List<Customer>> SearchCustomersAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await _Dbcontext.customers.ToListAsync();

            searchTerm = searchTerm.ToLower();

            return await _Dbcontext.customers
                .Where(c => c.Name.ToLower().Contains(searchTerm) ||
                            c.Address.ToLower().Contains(searchTerm) ||
                            c.Email.ToLower().Contains(searchTerm) ||
                            c.PhoneNumber.ToLower().Contains(searchTerm) ||
                            c.LicenceNumber.ToLower().Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<Customer> GetByEmailOrUsernameAsync(string identifier)
        {
            return await _Dbcontext.customers
                .FirstOrDefaultAsync(c => c.Email == identifier || c.UserName == identifier);
        }
    }
}

