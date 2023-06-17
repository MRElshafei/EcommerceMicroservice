using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider customersProvider;

        public CustomersController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await customersProvider.GetCustomersAsync();
            if (customers.IsSuccess)
            {

                return Ok(customers.Customers);

            }
            return NotFound("No Customer Found");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await customersProvider.GetCustomerAsync(id);
            if (customer.IsSuccess)
            {

                return Ok(customer.Customer);

            }
            return NotFound("No Customer Found");
        }
    }
}
