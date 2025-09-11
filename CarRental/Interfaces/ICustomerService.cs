using CarRental.DTOs;
using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.Interfaces

{
    public interface ICustomerService
    {
        Task<CustomerDto> GetCustomerIdAsync(Guid id);
        Task<List<CustomerDto>> GetAllCustomersAsync();
        Task AddCustomerAsync(CustomerDto dto);
        Task UpdateCustomerAsync(CustomerDto dto);
        Task DeleteCustomerAsync(Guid id);
        Task<List<CustomerDto>> SearchCustomersAsync(string searchTerm);
    }
}
