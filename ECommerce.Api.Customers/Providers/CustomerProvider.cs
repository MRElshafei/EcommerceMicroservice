using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomerProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;

        private readonly ILogger <CustomerProvider> logger;

        private readonly IMapper mapper;
        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {

                dbContext.Customers.Add(new Customer() { Id = 1, Name = "Mohamed", Address = "Aswan" });
                dbContext.Customers.Add(new Customer() { Id = 2, Name = "Ali", Address = "Luxor" });
                dbContext.Customers.Add(new Customer() { Id = 3, Name = "Hossam", Address = "Qena" });

                dbContext.SaveChanges();
            }
        }

        public CustomerProvider(CustomersDbContext dbContext, ILogger<CustomerProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        public async Task<(bool IsSuccess, CustomerDTO Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
           var customer =  dbContext.Customers.SingleOrDefault(x => x.Id == id);
            try
            {
                if (customer is not null)
                {
                    var result =   mapper.Map<Customer, CustomerDTO>(customer);
                    return (true, result, null);

                }
                return (false, null, "Data Not Found");
            }
            catch (Exception ex)
            {

                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }


        }

        public async Task<(bool IsSuccess, IEnumerable<CustomerDTO> Customers, string ErrorMessage)> GetCustomersAsync()
        {
      
            try
            {
                var customers = await dbContext.Customers.ToListAsync();
                if (customers.Any() && customers is not null)
                {
                    var result = mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);
                    return (true, result, null);
                }
                
                
                    return (false, null, "No Data Found");
                
            }
            catch (Exception ex)
            {

                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
           
        }




    }
}
