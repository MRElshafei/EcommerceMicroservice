using ECommerce.Api.Orders.Models;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<OrderDTO> Orders, string ErorrMessage)> GetOrdersAsync(int customerId);
    }
}
