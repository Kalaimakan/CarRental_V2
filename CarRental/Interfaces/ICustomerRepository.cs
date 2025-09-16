using CarRental.Models;
using System.Threading.Tasks;

namespace CarRental.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByIdAsync(Guid id);
        Task<List<Customer>> GetAllCustomersAsync();
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Customer customer);
        Task<List<Customer>> SearchCustomersAsync(string searchTerm);
        Task<Customer> GetByEmailOrUsernameAsync(string identifier);
    }
}
