using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Models;

namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<CustomerDTO> Customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, CustomerDTO Customer, string ErrorMessage)> GetCustomerAsync(int id);

    }
}
