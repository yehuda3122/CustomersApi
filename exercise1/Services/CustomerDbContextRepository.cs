
namespace exercise1.Services
{
    public class CustomerDbContextRepository(customerDbContext customers) : ICustomerRepository
    {
        private readonly customerDbContext customerDbContext = customers;

        public async Task<List<CustomerModel>> getAllCustomers()
        {
            var customers = await customerDbContext.Customers.ToListAsync<CustomerModel>();
            return customers;
        }
        public async Task<CustomerModel> addCustomer(CustomerModel customer)
        {
            await customerDbContext.Customers.AddAsync(customer);
            await customerDbContext.SaveChangesAsync();

            return customer;
        }



        public async Task<CustomerModel> getCustomerById(int id)
        {
            //var customer = getCustomer(id);
            var customer = await customerDbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new Exception(" - Customer not found with id: " + id);
            }
            return customer;
        }

        public async Task<int> deleteCustomer(int id)
        {

            //var customer = getCustomer(id);
            var customer = await customerDbContext.Customers.FindAsync(id);
            if (customer != null)
            {
                customerDbContext.Customers.Remove(customer);
            }
            await customerDbContext.SaveChangesAsync();
            return id;
        }

        public async Task<CustomerModel> updateCustomer(CustomerModel customerToUpdate)
        {
            customerDbContext.Customers.Update(customerToUpdate);
            await customerDbContext.SaveChangesAsync();
            return customerToUpdate;
        }

        //private async void writeInJson()
        //{
        //    var updatedJson = JsonSerializer.Serialize(customers, new JsonSerializerOptions { WriteIndented = true });
        //    await File.WriteAllTextAsync(_filePath, updatedJson);
        //}

        //public CustomerModel getCustomer(int id)
        //{
        //    var customer = customers.FirstOrDefault(c => c.GuId == id);
        //    if (customer == null)
        //    {
        //        throw new Exception(" - Customer not found with id: " + id);
        //    }
        //    return customer;
        //}
    }
}
